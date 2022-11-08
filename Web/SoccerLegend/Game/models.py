from django.db import models
from datetime import datetime

class Game (models.Model):
    info = models.TextField()
    redScore = models.IntegerField()
    blueScore = models.IntegerField()
    date = models.DateField(default = datetime.now)