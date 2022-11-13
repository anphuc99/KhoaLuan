import json
from channels.generic.websocket import WebsocketConsumer
from asgiref.sync import async_to_sync
from threading import Timer
from datetime import datetime
from Account.models import Account
from SoccerLegend.Global import Global
from .models import Game, GameInfo
from django.db import transaction
import random


class ChatConsumer(WebsocketConsumer):
    def connect(self):
        self.room_group_name = 'lobby'
        async_to_sync(self.channel_layer.group_add)(
            self.room_group_name,
            self.channel_name
        )

        self.accept()

    def receive(self, text_data):
        print("receive:" + text_data)
        text_data_json = json.loads(text_data)
        async_to_sync(self.channel_layer.send)(
            self.channel_name, text_data_json
        )

    def login(self, event):
        _token = event["data"]
        player = Account.objects.get(_token=_token)
        self._token = _token
        async_to_sync(self.channel_layer.group_add)(
            _token,
            self.channel_name
        )

    def createRoom(self, event):
        try:
            print("createRoom")
            data = event["data"]
            _token = data["_token"]
            roomID = data["roomID"]
            clientID = data["clientID"]
            player = Account.objects.get(_token=_token)
            room = {
                "Master": _token,
                "MasterClientID": clientID,
                "PlayerList": [],
            }
            Global.setValue(roomID, room)
            self.send(text_data=json.dumps({
                "type": "onServerCreateRoom",
                "data": "Success"
            }))
        except:
            self.send(text_data=json.dumps({
                "type": "onServerCreateRoom",
                "data": "Fail"
            }))

    def joinRoom(self, event):
        try:
            data = event["data"]
            _token = data["_token"]
            roomID = data["roomID"]
            clientID = data["clientID"]
            player = Account.objects.get(_token=_token)
            room = Global.getValue(roomID)
            room["PlayerList"].append({
                "id": player.id,
                "clientID": clientID
            })
            Global.setValue(roomID, room)
            self.roomID = roomID
            self.clientID = clientID
            async_to_sync(self.channel_layer.group_add)(
                roomID,
                self.channel_name
            )
            self.send(text_data=json.dumps({
                "type": "onServerJoinRoom",
                "data": "Success"
            }))
        except:
            self.send(text_data=json.dumps({
                "type": "onServerJoinRoom",
                "data": "Fail"
            }))

    def onGameBeginStart(self, event):
        print("onGameBeginStart")
        print(self._token)
        room = Global.getValue(self.roomID)
        if room["Master"] == self._token:
            PlayerList = room["PlayerList"]
            MaxPlayer = len(PlayerList)
            random.shuffle(PlayerList)
            i = 0
            room["PlayerTeam"] = []
            for player in PlayerList:
                print(player["clientID"])
                if (i < MaxPlayer/2):
                    room["PlayerTeam"].append({
                        "UserID": player["clientID"],
                        "team": 0,
                        "position": i,
                        "account_id": player["id"]
                    })
                else:
                    room["PlayerTeam"].append({
                        "UserID": player["clientID"],
                        "team": 1,
                        "position": i - MaxPlayer / 2,
                        "account_id": player["id"]
                    })
                i += 1
            Global.setValue(self.roomID, room)


            self.sendRoom(self.roomID, {
                "type": "receiveTeamFromSever",
                "data": json.dumps({"Items": room["PlayerTeam"]})
            })

    def connectWithMaster(self, event):
        room = Global.getValue(self.roomID)
        print(datetime.now())
        if room["Master"] == self._token:
            room["lastConnect"] = datetime.now().timestamp()
            Global.setValue(self.roomID, room)

    def playerOut(self, event):
        data = event["data"]
        roomID = data["roomID"]
        clientID = data["clientID"]
        _token = data["_token"]
        room = Global.getValue(roomID)
        MasterClientID = room["MasterClientID"]
        Master = room["Master"]
        if MasterClientID == clientID:
            lastConnect = room["lastConnect"]
            if lastConnect + 1 < datetime.now().timestamp():
                # set new master
                key = 0
                for k in range(len(room["PlayerList"])):
                    if room["PlayerList"][k]["clientID"] == clientID:
                        key = k
                        break
                room["PlayerList"].pop(key)

                for k in range(len(room["PlayerTeam"])):
                    if room["PlayerTeam"][k]["UserID"] == clientID:
                        key = k
                        break
                room["PlayerTeam"].pop(key)
                MaxPlayer = len(room["PlayerList"])
                rd = random.randint(0, MaxPlayer - 1)
                NewMaster = room["PlayerList"][rd]
                accont = Account.objects.get(id = NewMaster["id"])
                room["Master"] = accont._token
                room["MasterClientID"] = NewMaster["clientID"]
                Global.setValue(self.roomID, room)
                self.send(text_data=json.dumps({
                    "type": "setNewMaster",
                    "data": room["MasterClientID"]
                }))
        elif Master == _token:
            key = 0
            for k in range(len(room["PlayerList"])):
                if room["PlayerList"][k]["clientID"] == clientID:
                    key = k
                    break
            room["PlayerList"].pop(key)

            for k in range(len(room["PlayerTeam"])):
                if room["PlayerTeam"][k]["UserID"] == clientID:
                    key = k
                    break
            room["PlayerTeam"].pop(key)
            Global.setValue(self.roomID, room)

    def sendRoom(self, room, data):
        async_to_sync(self.channel_layer.group_send)(
            room, {
                "type": "sendData",
                "data": data
            })

    def sendData(self, event):
        data = event["data"]
        self.send(json.dumps(data))
        
    def sendResult(self, event):        
        data = event["data"]
        redScore = data["redScore"]
        blueScore = data["blueScore"]
        room = Global.getValue(self.roomID)
        if room["Master"] == self._token:
            with transaction.atomic():
                accountMaster = Account.objects.get(_token = self._token)
                game = Game.objects.create(redScore = redScore, blueScore = blueScore, master = accountMaster.id)
                PlayerTeam = room["PlayerTeam"]
                for player in PlayerTeam:
                    GameInfo.objects.create(gameID = game.id, playerID = player["account_id"], team = player["team"])
                self.sendRoom(self.roomID, {
                    "type": "endGame",
                    "data": room["MasterClientID"]
                })      
