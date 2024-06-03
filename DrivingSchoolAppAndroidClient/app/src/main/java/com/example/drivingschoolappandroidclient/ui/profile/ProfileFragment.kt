package com.example.drivingschoolappandroidclient.ui.profile

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import androidx.lifecycle.ViewModelProvider
import com.example.drivingschoolappandroidclient.App
import com.example.drivingschoolappandroidclient.databinding.FragmentProfileBinding
import com.example.drivingschoolappandroidclient.models.models.EditMeCallback
import com.example.drivingschoolappandroidclient.models.models.EditMeModel
import com.example.drivingschoolappandroidclient.models.models.UserRoles

class ProfileFragment : Fragment() {

    private lateinit var viewModel: ProfileViewModel
    private lateinit var binding: FragmentProfileBinding
    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        binding = FragmentProfileBinding.inflate(layoutInflater)
        viewModel = ViewModelProvider(this)[ProfileViewModel::class.java]
        with(binding){
            btnExit.setOnClickListener {
                App.controller.loginResponse.value = null
            }
            App.controller.loginResponse.observe(viewLifecycleOwner){
                tvRole.text = it!!.role
                if(it.role == UserRoles.admin){
                    trProfilePatronymic.visibility = View.GONE
                    trProfileFirstname.visibility = View.GONE
                    trProfileLastname.visibility = View.GONE
                }
                else {
                    trProfilePatronymic.visibility = View.VISIBLE
                    trProfileFirstname.visibility = View.VISIBLE
                    trProfileLastname.visibility = View.VISIBLE
                }
            }
            App.me.observe(viewLifecycleOwner){
                tvEmail.text = it!!.email
                etFirstname.setText(it.firstName)
                etLastname.setText(it.lastName)
                etPatronymic.setText(it.patronymic)
                etPhone.setText(it.phoneNumber)
                ivPfp.setImageBitmap(it.profilePicture)
            }
            btnSave.setOnClickListener {
                val phone = if(etPhone.text.isNotBlank())
                    etPhone.text.toString()
                else null
                viewModel.editMeModel = EditMeModel(
                    id = App.controller.loginResponse.value!!.id,
                    phoneNumber = phone,
                    email = tvEmail.text.toString(),
                    firstName = etFirstname.text.toString(),
                    lastName = etLastname.text.toString(),
                    patronymic = etPatronymic.text.toString(),
                    profilePictureBytes = null
                )
//                viewModel.editMeModel.profilePicture = (ivPfp.drawable as BitmapDrawable).bitmap

                App.blocked.value = true
                App.controller.api.editMe(
                    viewModel.editMeModel,
                    App.controller.loginResponse.value!!.authHead
                ).enqueue(EditMeCallback())
            }
            return root
        }
    }
    override fun onDestroy() {
        super.onDestroy()
        App.controller.loginResponse.removeObservers(this)
        App.me.removeObservers(this)
    }
}