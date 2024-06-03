package com.example.drivingschoolappandroidclient.models.models

import android.graphics.Bitmap
import android.graphics.BitmapFactory
import java.io.ByteArrayOutputStream

data class RegisterModel(
    var email: String,
    var password: String,
    override var firstName: String,
    override var lastName: String,
    override var patronymic: String? = null,
    var phoneNumber: String?,
    override var profilePictureBytes: ByteArray?,

    ) : IPerson{

    private var _image: Bitmap? = null
    override var profilePicture: Bitmap?
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
