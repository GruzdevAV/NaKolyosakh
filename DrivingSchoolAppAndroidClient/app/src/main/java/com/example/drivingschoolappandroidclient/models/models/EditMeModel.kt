package com.example.drivingschoolappandroidclient.models.models

import android.graphics.Bitmap
import android.graphics.BitmapFactory
import java.io.ByteArrayOutputStream

data class EditMeModel(
    var id: String,
    var phoneNumber: String?,
    var email: String?,
    var firstName: String?,
    var lastName: String?,
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
}
