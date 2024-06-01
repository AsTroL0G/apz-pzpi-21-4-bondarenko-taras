package com.example.cargotruckmobile.Models


data class Cargo(
    val id: String,
    val name: String,
    val weight: Float,
    val length: Float,
    val height: Float,
    val width: Float,
    val announcedPrice: Float,
    val shippingPrice: Float,
    val noticeId: String?,
    val senderId: String,
    val receiverId: String,
    val citySenderId: String,
    val addressSenderId: String,
    val cityReceiverId: String,
    val addressReceiverId: String,
    val status: String
)
data class CargoListResponse(
    val items: List<Cargo>,
    val pageNumber: Int,
    val pageSize: Int,
    val totalPages: Int,
    val totalItems: Int,
    val hasPreviousPage: Boolean,
    val hasNextPage: Boolean
)
data class CargoCreateDto(
    val name: String,
    val weight: Float,
    val length: Float,
    val height: Float,
    val width: Float,
    val announcedPrice: Float,
    val senderId: String,
    val receiverId: String,
    val citySenderId: String,
    val addressSenderId: String,
    val cityReceiverId: String,
    val addressReceiverId: String
)
data class DetailsCargo(
    val id: String,
    val name: String,
    val weight: Int,
    val length: Int,
    val height: Int,
    val width: Int,
    val announcedPrice: Int,
    val shippingPrice: Int,
    val noticeId: String?,
    val senderId: String,
    val receiverId: String,
    val citySenderId: String,
    val addressSenderId: String,
    val cityReceiverId: String,
    val addressReceiverId: String,
    val status: String
)

data class CargoUpdateNotice(
    val cargoId: String,
    val newNoticeId: String,

    )
