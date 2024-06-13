package com.example.drivingschoolappandroidclient.ui.students

import android.R
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.ArrayAdapter
import androidx.fragment.app.Fragment
import androidx.lifecycle.ViewModelProvider
import androidx.recyclerview.widget.LinearLayoutManager
import com.example.drivingschoolappandroidclient.App
import com.example.drivingschoolappandroidclient.databinding.FragmentStudentsBinding
import com.example.drivingschoolappandroidclient.models.models.Instructor
import com.example.drivingschoolappandroidclient.models.models.InstructorStudentPairModel
import com.example.drivingschoolappandroidclient.models.models.SetInstructorToStudentCallback
import com.example.drivingschoolappandroidclient.models.models.UserRoles

class StudentsFragment : Fragment() {

    private lateinit var binding: FragmentStudentsBinding
    private lateinit var viewModel: StudentsViewModel

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        binding = FragmentStudentsBinding.inflate(layoutInflater)
        App.studentRatings.observe(viewLifecycleOwner){
            val size = maxOf(viewModel.studentsAdapter.students?.size?:0, it?.size?:0)
            viewModel.studentsAdapter.notifyItemRangeChanged(0,size)
        }
        App.myStudentRatings.observe(viewLifecycleOwner){
            val size = maxOf(viewModel.studentsAdapter.students?.size?:0, it?.size?:0)
            viewModel.studentsAdapter.notifyItemRangeChanged(0,size)
        }
        App.instructors.observe(viewLifecycleOwner){
            viewModel.instructorAdapter?.notifyDataSetChanged()
        }
        viewModel = ViewModelProvider(this)[StudentsViewModel::class.java]
        App.instructors.observe(viewLifecycleOwner){
            viewModel.instructorAdapter = viewModel.instructorAdapter ?: ArrayAdapter(
                requireContext(),
                R.layout.simple_spinner_item,
                it ?: listOf()
            )
        }
        with(binding){
            spStudentsInstructors.adapter = viewModel.instructorAdapter
            rvStudents.layoutManager = LinearLayoutManager(
                this@StudentsFragment.context,LinearLayoutManager.VERTICAL,false
            )
            rvStudents.adapter = viewModel.studentsAdapter
            btnSetInstructor.setOnClickListener {
                val instructor = spStudentsInstructors.selectedItem as Instructor?
                val student = viewModel.studentsAdapter.selected
                if (instructor!=null && student!=null){
                    App.block()
                    App.controller.api.setInstructorToStudent(InstructorStudentPairModel(
                        instructor.instructorId,
                        student.studentId
                    ),
                    App.controller.loginResponse.value!!.authHead
                    ).enqueue(SetInstructorToStudentCallback())
                }
            }
            tbShowMyStudents.setOnCheckedChangeListener { buttonView, isChecked ->
                viewModel.studentsAdapter.showMyStudents = isChecked
            }
            when(App.controller.loginResponse.value!!.role){
                UserRoles.instructor -> llStudentsForInstructor.visibility = View.VISIBLE
                UserRoles.admin -> llStudentsForAdmin.visibility = View.VISIBLE
            }
            return root
        }
    }

    override fun onDestroy() {
        super.onDestroy()
        App.studentRatings.removeObservers(this)
        App.myStudentRatings.removeObservers(this)
        App.instructors.removeObservers(this)
    }
}