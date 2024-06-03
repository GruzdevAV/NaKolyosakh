package com.example.drivingschoolappandroidclient.models.models

import java.sql.Date

data class LoginResponse(
    val token: String,
    val expiration: Date,
    val role: String,
    val id: String,
) {
    val authHead get() = "Bearer $token"
}