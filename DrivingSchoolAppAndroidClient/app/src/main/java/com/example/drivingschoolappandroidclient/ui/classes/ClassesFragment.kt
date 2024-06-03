package com.example.drivingschoolappandroidclient.ui.classes

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.ArrayAdapter
import androidx.fragment.app.Fragment
import androidx.lifecycle.ViewModelProvider
import androidx.navigation.fragment.findNavController
import androidx.recyclerview.widget.LinearLayoutManager
import com.example.drivingschoolappandroidclient.App
import com.example.drivingschoolappandroidclient.R
import com.example.drivingschoolappandroidclient.databinding.FragmentClassesBinding
import com.example.drivingschoolappandroidclient.models.models.CancelClassCallback
import com.example.drivingschoolappandroidclient.models.models.ClassStudentPairModel
import com.example.drivingschoolappandroidclient.models.models.SetClassCallback
import com.example.drivingschoolappandroidclient.models.models.Student
import com.example.drivingschoolappandroidclient.models.models.UserRoles

class ClassesFragment : Fragment() {

    private lateinit var binding: FragmentClassesBinding
    private lateinit var viewModel: ClassesViewModel

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        binding = FragmentClassesBinding.inflate(layoutInflater)
        viewModel = ViewModelProvider(this).get(ClassesViewModel::class.java)
        App.classes.observe(viewLifecycleOwner){
            val size = maxOf(viewModel.classesAdapter.classes?.size?:0, it?.size?:0)
            viewModel.classesAdapter.notifyItemRangeChanged(0, size)
        }
        App.myClasses.observe(viewLifecycleOwner){
            val size = maxOf(viewModel.classesAdapter.classes?.size?:0, it?.size?:0)
            viewModel.classesAdapter.notifyItemRangeChanged(0, size)
        }
        App.myStudents.observe(viewLifecycleOwner){
            viewModel.studentAdapter = viewModel.studentAdapter ?: ArrayAdapter(
                requireContext(),
                android.R.layout.simple_spinner_item,
                it ?: listOf()
            )
        }
        with(binding){
            spnStudents.adapter = viewModel.studentAdapter
            btnCancelClass.setOnClickListener {
                App.blocked.value = true
                App.controller.api.cancelClass(
                    viewModel.classesAdapter.selected!!.classId,
                    App.authHead
                ).enqueue(CancelClassCallback())
            }
            btnAddGrade.setOnClickListener {
                val values = Bundle()
                values.putInt("classId",viewModel.classesAdapter.selected!!.classId)
                findNavController().navigate(
                    R.id.action_nav_classes_to_post_grade,
                    values
                )
            }
            btnClassesAddStudent.setOnClickListener {
                App.blocked.value = true
                val studentId = if(App.controller.loginResponse.value!!.role == UserRoles.student)
                    App.students.value?.first {
                        it.userId == App.controller.loginResponse.value!!.id
                    }?.studentId
                else{
                    (spnStudents.selectedItem as Student?)?.studentId
                }
                App.controller.api.setClass(
                    ClassStudentPairModel(
                        viewModel.classesAdapter.selected!!.classId,
                        studentId ?: -1
                    ),
                    App.authHead
                ).enqueue(SetClassCallback())
            }
            tbShowMyClasses.setOnCheckedChangeListener { buttonView, isChecked ->
                rvClasses.recycledViewPool.clear()
                viewModel.classesAdapter.showMyClasses = isChecked
                (if(isChecked) View.VISIBLE else View.GONE). also {
                    btnAddGrade.visibility = it
                    llClassesAddStudent.visibility = it
                    btnCancelClass.visibility = it
                }
            }
            (if(viewModel.classesAdapter.showMyClasses)
                View.VISIBLE else View.GONE).also {
                btnAddGrade.visibility = it
                llClassesAddStudent.visibility = it
                btnCancelClass.visibility = it
            }
            App.controller.loginResponse.observe(viewLifecycleOwner){
                when(it?.role){
                    UserRoles.admin -> {
                        llClassesForStudentsAndInstructors.visibility = View.GONE
                    }
                    UserRoles.student -> {
                        llClassesForStudentsAndInstructors.visibility = View.VISIBLE
                        spnStudents.visibility = View.GONE
                        btnClassesAddStudent.text = "Записать себя"
                    }
                    UserRoles.instructor -> {
                        llClassesForStudentsAndInstructors.visibility = View.VISIBLE
                        spnStudents.visibility = View.VISIBLE
                        btnClassesAddStudent.text = "Записать ученика"
                    }
                }
            }
            rvClasses.layoutManager = LinearLayoutManager(
                context,LinearLayoutManager.VERTICAL,false
            )
            rvClasses.adapter = viewModel.classesAdapter
            return root
        }
    }

    override fun onDestroy() {
        super.onDestroy()
        App.classes.removeObservers(this)
        App.myClasses.removeObservers(this)
        App.students.removeObservers(this)
        App.controller.loginResponse.removeObservers(this)
    }
}