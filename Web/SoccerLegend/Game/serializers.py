from rest_framework import serializers
    

class resutls(serializers.Serializer):
    playerTeams = serializers.CharField()
    redScore = serializers.IntegerField()
    blueScore = serializers.IntegerField()

            