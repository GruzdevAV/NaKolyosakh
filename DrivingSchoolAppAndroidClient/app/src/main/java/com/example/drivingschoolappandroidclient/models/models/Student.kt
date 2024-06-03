package com.example.drivingschoolappandroidclient.models.models

import android.graphics.Bitmap

open class Student(
    var studentId: Int,
    var userId: String,
    var user: ApplicationUser,
    var instructorId: Int? = null,
    var instructor: Instructor? = null
) : IPerson {

    override var firstName
        get() = user.firstName
        set(value) {user.firstName = value }
    override var lastName: String
        get() = user.lastName
        set(value) {user.lastName = value}
    override var patronymic: String?
        get() = user.patronymic
        set(value) {user.patronymic = value}
    override var profilePictureBytes: ByteArray?
        get() = user.profilePictureBytes
        set(value) {user.profilePictureBytes = value}
    override var profilePicture: Bitmap?
        get() = user.profilePicture
        set(value) {user.profilePicture = value}
    override fun toString() = "$lastName ${firstName}${patronymic?.let { " $patronymic" } } - №$studentId"
}
