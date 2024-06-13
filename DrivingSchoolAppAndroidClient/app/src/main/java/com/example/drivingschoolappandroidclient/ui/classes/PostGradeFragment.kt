package com.example.drivingschoolappandroidclient.ui.classes

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.ArrayAdapter
import androidx.fragment.app.Fragment
import androidx.lifecycle.ViewModelProvider
import androidx.navigation.fragment.findNavController
import com.example.drivingschoolappandroidclient.App
import com.example.drivingschoolappandroidclient.databinding.FragmentPostGradeBinding
import com.example.drivingschoolappandroidclient.models.models.Class
import com.example.drivingschoolappandroidclient.models.models.GradeByInstructorToStudentModel
import com.example.drivingschoolappandroidclient.models.models.GradeByStudentToInstructorModel
import com.example.drivingschoolappandroidclient.models.models.GradesByInstructorsToStudents
import com.example.drivingschoolappandroidclient.models.models.GradesByStudentsToInstructors
import com.example.drivingschoolappandroidclient.models.models.PostGradeToInstructorCallback
import com.example.drivingschoolappandroidclient.models.models.PostGradeToStudentCallback
import com.example.drivingschoolappandroidclient.models.models.UserRoles

class PostGradeFragment : Fragment() {

    private lateinit var binding: FragmentPostGradeBinding
    private lateinit var viewModel: PostGradeViewModel
    private lateinit var `class` : Class
    private var fromWhoId : Int? = null
    private var toWhomId : Int? = null
    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        val classId = arguments?.getInt("classId")
        `class` = App.classes.value!!.first { it.classId == classId }
        viewModel = ViewModelProvider(this).get(PostGradeViewModel::class.java)
        with(viewModel){
            gradesByStudentAdapter = gradesByStudentAdapter ?: ArrayAdapter(
                requireContext(),
                android.R.layout.simple_spinner_item,
                GradesByStudentsToInstructors.values()
            )
            gradesByInstructorAdapter = gradesByInstructorAdapter ?: ArrayAdapter(
                requireContext(),
                android.R.layout.simple_spinner_item,
                GradesByInstructorsToStudents.values()
            )
        }
        binding = FragmentPostGradeBinding.inflate(layoutInflater)
        with(binding){
            btnPostGradeCancel.setOnClickListener {
                findNavController().popBackStack()
            }
            tvPostGradeClass.text = `class`.toString()
            when(App.controller.loginResponse.value?.role){
                UserRoles.student -> {
                    spnGrade.adapter = viewModel.gradesByStudentAdapter
                    tvPostGradeToWhoTitle.text = "Инструктор: "
                    tvPostGradeToWhoText.text = `class`.instructor.toString()
                    btnPostGradeOk.setOnClickListener {
                        App.block()
                        App.controller.api.postGradeToInstructorForClass(
                            GradeByStudentToInstructorModel(
                                `class`.classId,
                                (spnGrade.selectedItem as GradesByStudentsToInstructors).value,
                                etPostGradeComment.text.toString()
                            ),
                            App.authHead
                        ).enqueue(PostGradeToInstructorCallback())
                        findNavController().popBackStack()
                    }
                }
                UserRoles.instructor -> {
                    spnGrade.adapter = viewModel.gradesByInstructorAdapter
                    tvPostGradeToWhoTitle.text = "Ученик: "
                    tvPostGradeToWhoText.text = `class`.student.toString()
                    btnPostGradeOk.setOnClickListener {
                        App.block()
                        App.controller.api.postGradeToStudentForClass(
                            GradeByInstructorToStudentModel(
                                `class`.classId,
                                (spnGrade.selectedItem as GradesByInstructorsToStudents).value,
                                etPostGradeComment.text.toString()
                            ),
                            App.authHead
                        ).enqueue(PostGradeToStudentCallback())
                        findNavController().popBackStack()
                    }

                }
            }
            return root
        }
    }
}

