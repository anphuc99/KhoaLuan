from django.shortcuts import render
from rest_framework.views import APIView
from .serializers import resutls
from .models import Game
from Player.models import Player
from rest_framework.response import Response
from rest_framework import status
import json
from types import SimpleNamespace

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
        
        return Response(data="ok", status=status.HTTP_200_OK)    