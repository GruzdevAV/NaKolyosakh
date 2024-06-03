package com.example.drivingschoolappandroidclient.models.models

import android.graphics.Bitmap

interface IPerson {
    var firstName: String
    var lastName: String
    var patronymic: String?
    var profilePictureBytes: ByteArray?
    var profilePicture: Bitmap?
}