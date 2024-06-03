package com.example.drivingschoolappandroidclient.models.models

import android.graphics.Bitmap
import android.graphics.BitmapFactory
import java.io.ByteArrayOutputStream

data class ApplicationUser(
    var id: String,
    var userName: String,
    var normalizedUserName: String,
    var email: String,
    var normalizedEmail: String,
    var emailConfirmed: Boolean,
    var passwordHash: String,
    var securityStamp: String,
    var concurrencyStamp: String,
    var phoneNumber: String?,
    var phoneNumberConfirmed: Boolean,
    var twoFactorEnabled: Boolean,
    var lockoutEnd: String,
    var lockoutEnabled: Boolean,
    var accessFailedCount: Int,
    var firstName: String,
    var lastName: String,
    var patronymic: String?,
    var profilePictureBytes: ByteArray?,
){
    private var _image: Bitmap? = null
    var profilePicture: Bitmap?
        get() {
            if (_image == null){
                profilePictureBytes?.let{
                    _image = BitmapFactory.decodeByteArray(it,0,it.size)
                }
            }
            return _image
        }
        set(value) {
            _image = value
            value?.let{
                val os = ByteArrayOutputStream()
                it.compress(Bitmap.CompressFormat.JPEG,100,os)
                profilePictureBytes = os.toByteArray()
            }
        }

    override fun toString() =
        "$lastName ${firstName}${patronymic?.let { " $patronymic" }?:""}"
}