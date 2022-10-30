# Generated by Django 4.0.4 on 2022-10-30 09:23

from django.db import migrations, models


class Migration(migrations.Migration):

    initial = True

    dependencies = [
    ]

    operations = [
        migrations.CreateModel(
            name='Player',
            fields=[
                ('id', models.BigAutoField(auto_created=True, primary_key=True, serialize=False, verbose_name='ID')),
                ('account_id', models.IntegerField()),
                ('name', models.CharField(default='', max_length=100)),
                ('score', models.IntegerField()),
            ],
        ),
    ]
