from rest_framework import serializers
from .models import Player

class ChooseCharater(serializers.Serializer):
    name = serializers.CharField(max_length=100)
    _token = serializers.CharField(max_length=100)
    
class PlayerSeri (serializers.ModelSerializer):
    class Meta:
        model = Player
        fields = ("id", "name", "account_id", "score")
        
class AccountID (serializers.Serializer):
    account_id = serializers.IntegerField()