# Generated by Django 4.0.4 on 2022-10-30 09:57

from django.db import migrations, models


class Migration(migrations.Migration):

    dependencies = [
        ('Player', '0001_initial'),
    ]

    operations = [
        migrations.AlterField(
            model_name='player',
            name='score',
            field=models.IntegerField(default=0),
        ),
    ]
