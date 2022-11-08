# Generated by Django 4.0.4 on 2022-11-08 10:04

import datetime
from django.db import migrations, models


class Migration(migrations.Migration):

    initial = True

    dependencies = [
    ]

    operations = [
        migrations.CreateModel(
            name='Game',
            fields=[
                ('id', models.BigAutoField(auto_created=True, primary_key=True, serialize=False, verbose_name='ID')),
                ('info', models.TextField()),
                ('redScore', models.IntegerField()),
                ('blueScore', models.IntegerField()),
                ('date', models.DateField(default=datetime.datetime.now)),
            ],
        ),
    ]
