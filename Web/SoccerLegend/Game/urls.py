from django.urls import path

from . import views

urlpatterns = [
    # path("send-game-resutls", views.GameResutls.as_view(), name="send-game-resutls"),
    path("get-top-rank", views.GetTopRank.as_view(), name="get-top-rank"),
    path("get-my-rank/<int:account_id>", views.GetMyRank.as_view(), name="get-my-rank"),
]
