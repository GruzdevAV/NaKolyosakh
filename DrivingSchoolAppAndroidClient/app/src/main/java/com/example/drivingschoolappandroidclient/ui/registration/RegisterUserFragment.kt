package com.example.drivingschoolappandroidclient.ui.registration

import android.os.Bundle
import android.util.Patterns.EMAIL_ADDRESS
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.core.widget.doAfterTextChanged
import androidx.fragment.app.Fragment
import androidx.lifecycle.ViewModelProvider
import com.example.drivingschoolappandroidclient.App
import com.example.drivingschoolappandroidclient.databinding.FragmentRegisterUserBinding
import com.example.drivingschoolappandroidclient.models.models.RegisterInstructorCallback
import com.example.drivingschoolappandroidclient.models.models.RegisterModel
import com.example.drivingschoolappandroidclient.models.models.RegisterStudentCallback

class RegisterUserFragment : Fragment() {

    private lateinit var binding: FragmentRegisterUserBinding
    private lateinit var viewModel: RegisterUserViewModel

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {

        binding = FragmentRegisterUserBinding.inflate(layoutInflater)
        viewModel = ViewModelProvider(this)[RegisterUserViewModel::class.java]
        checkValuesChanged()
        with(binding){
            etRegEmail.doAfterTextChanged { checkValuesChanged() }
            etRegLastname.doAfterTextChanged { checkValuesChanged() }
            etRegFirstname.doAfterTextChanged { checkValuesChanged() }
            etRegPassword.doAfterTextChanged { checkValuesChanged() }
            etRegPhone.doAfterTextChanged { checkValuesChanged() }
            
            btnRegister.setOnClickListener {
                App.block()
                val model = RegisterModel(
                    email = etRegEmail.text.toString(),
                    lastName = etRegLastname.text.toString(),
                    patronymic = etRegPatronymic.text.toString(),
                    firstName = etRegFirstname.text.toString(),
                    password = etRegPassword.text.toString(),
                    phoneNumber = etRegPhone.text.toString(),
                    profilePictureBytes = null
                )
//                model.profilePicture = (ibRegImage.drawable as BitmapDrawable).bitmap

                if (rbInstructor.isChecked)
                    App.controller.api.registerInstructor(
                        model,App.controller.loginResponse.value!!.authHead
                    ).enqueue(RegisterInstructorCallback())
                else if (rbStudent.isChecked)
                    App.controller.api.registerStudent(
                        model,App.controller.loginResponse.value!!.authHead
                    ).enqueue(RegisterStudentCallback())
                else App.unblock()
            }
            return root
        }
    }

    private fun checkValuesChanged() {
        with(binding){
            btnRegister.isEnabled = etRegLastname.text.isNotEmpty()
                    && etRegFirstname.text.isNotEmpty()
                    && etRegPassword.text.isNotEmpty()
                    && EMAIL_ADDRESS.matcher(etRegEmail.text).matches()
                    && etRegPhone.text.isNotEmpty()
                    && (rbStudent.isChecked || rbInstructor.isChecked)
        }
    }

}