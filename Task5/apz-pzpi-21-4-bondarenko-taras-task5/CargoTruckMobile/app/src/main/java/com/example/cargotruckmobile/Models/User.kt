package com.example.cargotruckmobile.Models


class User(
    val firstName: String,
    val lastName: String,
    val patronym: String,
    val roles: Array<Roles>,
    val phoneNumber: String,
    val email: String,
    val password: String
)
class UserCreate(
    val firstName: String,
    val lastName: String,
    val patronym: String,
    val phoneNumber: String,
    val email: String,
    val password: String
)
data class UserListResponse(
    val items: List<UserDto>,
    val pageNumber: Int,
    val pageSize: Int,
    val totalPages: Int,
    val totalItems: Int,
    val hasPreviousPage: Boolean,
    val hasNextPage: Boolean
)
class UserDto(
    val id: String,
    val firstName: String,
    val lastName: String,
    val patronym: String,
    val roles: Array<Roles>,
    val phoneNumber: String,
    val email: String,
    val password: String
)
class UserUpdate(
    val id: String,
    val firstName: String,
    val lastName: String,
    val patronym: String,
    val phoneNumber: String,
    val email: String,
    val password: String
)

class LoginResponse(
    val accessToken: String,
    val refreshToken: String
)


class Credentials(val email: String, val password: String)