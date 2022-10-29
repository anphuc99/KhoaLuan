import email
from email.policy import default
from django.db import models

# Create your models here.
class Account(models.Model):
    name = models.CharField(max_length=100)
    email = models.CharField(max_length=100)
    username = models.CharField(max_length=100)
    password = models.CharField(max_length=100)
    _token = models.CharField(max_length=200)
    isUse = models.BooleanField(default = True)
    
class Player (models.Model):
    account = models.ForeignKey(Account, on_delete=models.CASCADE)
    level = models.IntegerField()
    exp = models.IntegerField()
    money = models.IntegerField()
    characters = models.TextField()
    skins = models.TextField()
    pets = models.TextField()
    stadiums = models.TextField()
    score = models.IntegerField()
    speed = models.IntegerField()
    jump = models.IntegerField()
    energy = models.IntegerField(default = 120)