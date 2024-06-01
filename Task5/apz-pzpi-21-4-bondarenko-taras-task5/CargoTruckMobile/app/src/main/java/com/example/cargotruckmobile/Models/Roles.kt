package com.example.cargotruckmobile.Models

data class Roles(
    val id: String,
    val createdById: String,
    val createdDateUtc: String,
    val isDeleted: Boolean,
    val lastModifiedById: String?,
    val lastModifiedDateUtc: String?,
    val name: String
)