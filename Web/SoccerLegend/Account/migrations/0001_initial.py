# Generated by Django 4.0.4 on 2022-10-30 09:23

from django.db import migrations, models


class Migration(migrations.Migration):

    initial = True

    dependencies = [
    ]

    operations = [
        migrations.CreateModel(
            name='Account',
            fields=[
                ('id', models.BigAutoField(auto_created=True, primary_key=True, serialize=False, verbose_name='ID')),
                ('name', models.CharField(max_length=100)),
                ('email', models.CharField(max_length=100)),
                ('username', models.CharField(max_length=100, unique=True)),
                ('password', models.CharField(max_length=35)),
                ('_token', models.CharField(max_length=100, unique=True)),
                ('isUse', models.BooleanField(default=True)),
            ],
        ),
    ]
