from django.shortcuts import render
from rest_framework.views import APIView
from .serializers import resutls, Room, MasterClientOut
from .models import Game
from Player.models import Player
from Account.models import Account
from rest_framework.response import Response
from rest_framework import status
import json
from types import SimpleNamespace
from SoccerLegend.Global import Global

# Create your views here.

class GameResutls(APIView):
    def post(self, req):
        data = resutls(data= req.data)
        if not data.is_valid():
            return Response(data="not ok", status=status.HTTP_400_BAD_REQUEST)    
        Items = json.loads(data["playerTeams"].value, object_hook=lambda d: SimpleNamespace(**d))
        info = []
        for Item in Items.Items:
            info.append({
                "account_id": Item.account_id,
                "team": Item.team
            })
        JsonInfo = json.dumps(info)
        game = Game.objects.create(info = JsonInfo, redScore = data["redScore"].value, blueScore = data["blueScore"].value)
        for item in info:
            player = Player.objects.get(account_id = item["account_id"])
            history = player.history
            if history == "":
                player.history = str(game.id)
            else:
                player.history = history + "," + str(game.id)
            redScore = data["redScore"].value
            blueScore = data["blueScore"].value
            
            if redScore > blueScore and item["team"] == 0:
                player.score = player.score + 1
            elif redScore < blueScore and item["team"] == 1:
                player.score = player.score + 1
            elif redScore == blueScore:
                player.score = player.score + 1            
            player.save()
        
        return Response(status=status.HTTP_200_OK)    
    
class CreateRoom(APIView):
    def post(self, req):
        data = Room(data = req.data)
        if not data.is_valid():
            return Response(data="not ok", status=status.HTTP_400_BAD_REQUEST)    
        _token = data["_token"].value
        roomID = data["roomID"].value 
        player = Account.objects.get(_token = _token)     
        room = {
            "Master": _token,
            "PlayerList": [],
            "confirm": 0,
            "newMaster": 0
        }
        Global.setValue(roomID, room)
        return Response(status=status.HTTP_200_OK)
        
class JoinRoom(APIView):
    def post(self, req):
        data = Room(data = req.data)
        if not data.is_valid():
            return Response(data="not ok", status=status.HTTP_400_BAD_REQUEST)    
        _token = data["_token"].value
        roomID = data["roomID"].value
        player = Account.objects.get(_token = _token)
        room = Global.getValue(roomID)
        room["PlayerList"].append(_token)
        Global.setValue(roomID,room)
        return Response(status=status.HTTP_200_OK)
    
class MasterClientOutGame(APIView):
    def post(self, req):
        data = MasterClientOut(data= req.data)
        if not data.is_valid():
            return Response(data="not ok", status=status.HTTP_400_BAD_REQUEST)   
        _token = data["_token"].value
        roomID = data["roomID"].value
        newMaster = data["newMaster"].value
        room = Global.getValue(roomID)
        if _token in room["PlayerList"]:
            if room["newMaster"] == 0 or room["newMaster"] == newMaster:
                room["confirm"] += 1
                room["newMaster"] = newMaster
            else:
                return Response(status=status.HTTP_400_BAD_REQUEST)
        else:
            return Response(status=status.HTTP_400_BAD_REQUEST)        
        Global.setValue(roomID,room)
        return Response(status=status.HTTP_200_OK)


class TestSession(APIView):
    def post(self, req):
        Global.setValue("abc","lalalal")
        return Response(status=status.HTTP_200_OK)
    def get(self, req):
        ss = Global.getValue("abc")
        return Response(data=ss ,status=status.HTTP_200_OK)
    
def lobby(request):
    return render(request, 'chat/lobby.html')
        
        