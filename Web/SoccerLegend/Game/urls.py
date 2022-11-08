from django.urls import path

from . import views

urlpatterns = [
    path("send-game-resutls", views.GameResutls.as_view(), name="send-game-resutls"),
]
