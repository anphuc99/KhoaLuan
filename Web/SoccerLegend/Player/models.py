from django.db import models
from Account.models import Account

class Player (models.Model):
    account_id = models.IntegerField()
    name = models.CharField(max_length=100, default = "")
    # level = models.IntegerField()
    # exp = models.IntegerField()
    # money = models.IntegerField()
    # characters = models.TextField()
    # skins = models.TextField()
    # pets = models.TextField()
    # stadiums = models.TextField()
    score = models.IntegerField(default = 0)
    # speed = models.IntegerField()
    # jump = models.IntegerField()
    # energy = models.IntegerField(default = 120)
    # coefficient = models.IntegerField(default = 0)