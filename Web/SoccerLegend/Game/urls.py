from django.urls import path

from . import views

urlpatterns = [
    path("send-game-resutls", views.GameResutls.as_view(), name="send-game-resutls"),
    path("test", views.TestSession.as_view(), name="test"),
    path("create-room", views.CreateRoom.as_view(), name="create-room"),
    path("join-room", views.JoinRoom.as_view(), name="join-room"),
    path("master-out-room", views.MasterClientOutGame.as_view(), name="master-out-room"),
    path("chat", views.lobby, name="chat"),
]
