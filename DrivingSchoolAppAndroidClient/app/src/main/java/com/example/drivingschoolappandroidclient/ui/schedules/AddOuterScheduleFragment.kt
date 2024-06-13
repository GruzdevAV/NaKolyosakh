package com.example.drivingschoolappandroidclient.ui.schedules

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.core.widget.doAfterTextChanged
import androidx.fragment.app.Fragment
import androidx.lifecycle.ViewModelProvider
import androidx.navigation.fragment.findNavController
import com.example.drivingschoolappandroidclient.App
import com.example.drivingschoolappandroidclient.databinding.FragmentAddOuterScheduleBinding
import com.example.drivingschoolappandroidclient.models.models.AddOuterScheduleCallback
import com.example.drivingschoolappandroidclient.models.models.AddOuterScheduleModel

class AddOuterScheduleFragment : Fragment() {

    private lateinit var viewModel: AddOuterScheduleViewModel
    private lateinit var binding: FragmentAddOuterScheduleBinding

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        binding = FragmentAddOuterScheduleBinding.inflate(layoutInflater)
        viewModel = ViewModelProvider(this)[AddOuterScheduleViewModel::class.java]
        onTextChanged()
        with(binding){

            etIdGoogleSheet.doAfterTextChanged { onTextChanged() }
            etPageName.doAfterTextChanged { onTextChanged() }
            etTimeRange.doAfterTextChanged { onTextChanged() }
            etDateRange.doAfterTextChanged { onTextChanged() }
            etClassesRange.doAfterTextChanged { onTextChanged() }
            etFreeRange.doAfterTextChanged { onTextChanged() }
            etNotFreeRange.doAfterTextChanged { onTextChanged() }
            etYearRange.doAfterTextChanged { onTextChanged() }

            btnAddOuterScheduleOk.setOnClickListener {
                App.block()
                App.controller.api.addOuterScheduleToMe(
                    AddOuterScheduleModel(
                        googleSheetId = etIdGoogleSheet.text.toString(),
                        googleSheetPageName = etPageName.text.toString(),
                        timesOfClassesRange = etTimeRange.text.toString(),
                        datesOfClassesRange = etDateRange.text.toString(),
                        classesRange = etClassesRange.text.toString(),
                        freeClassExampleRange = etFreeRange.text.toString(),
                        notFreeClassExampleRange = etNotFreeRange.text.toString(),
                        yearRange = etYearRange.text.toString(),
                        userId = App.controller.loginResponse.value!!.id
                    ),
                    App.controller.loginResponse.value?.authHead ?: ""
                ).enqueue(AddOuterScheduleCallback())
                findNavController().popBackStack()
            }
            btnAddOuterScheduleCancel.setOnClickListener {
                findNavController().popBackStack()
            }
            return root
        }
    }

    private fun onTextChanged() {
        with(binding) {
            btnAddOuterScheduleOk.isEnabled = etIdGoogleSheet.text.isNotEmpty() &&
                    etPageName.text.isNotEmpty() && etTimeRange.text.isNotEmpty() &&
                    etDateRange.text.isNotEmpty() && etClassesRange.text.isNotEmpty() &&
                    etFreeRange.text.isNotEmpty() && etNotFreeRange.text.isNotEmpty() &&
                    etYearRange.text.isNotEmpty()
        }
    }

}