# Generated by Django 4.0.4 on 2022-11-08 08:26

from django.db import migrations, models


class Migration(migrations.Migration):

    initial = True

    dependencies = [
    ]

    operations = [
        migrations.CreateModel(
            name='Player',
            fields=[
                ('account_id', models.IntegerField(primary_key=True, serialize=False)),
                ('name', models.CharField(default='', max_length=100)),
                ('score', models.IntegerField(default=0)),
            ],
        ),
    ]
