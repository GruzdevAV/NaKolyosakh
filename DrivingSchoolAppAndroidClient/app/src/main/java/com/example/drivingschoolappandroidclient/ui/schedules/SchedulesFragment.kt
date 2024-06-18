package com.example.drivingschoolappandroidclient.ui.schedules

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import androidx.lifecycle.ViewModelProvider
import androidx.navigation.fragment.findNavController
import androidx.recyclerview.widget.LinearLayoutManager
import com.example.drivingschoolappandroidclient.App
import com.example.drivingschoolappandroidclient.R
import com.example.drivingschoolappandroidclient.databinding.FragmentSchedulesBinding
import com.example.drivingschoolappandroidclient.models.models.UserRoles

class SchedulesFragment : Fragment() {

    private lateinit var binding: FragmentSchedulesBinding
    private lateinit var viewModel: SchedulesViewModel

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        binding = FragmentSchedulesBinding.inflate(layoutInflater)
        viewModel = ViewModelProvider(this).get(SchedulesViewModel::class.java)
        App.schedules.observe(viewLifecycleOwner){
            val size = maxOf(viewModel.schedulesAdapter.schedules?.size?:0, it?.size?:0)
            viewModel.schedulesAdapter.notifyItemRangeChanged(0, size)
        }
        App.mySchedules.observe(viewLifecycleOwner){
            val size = maxOf(viewModel.schedulesAdapter.schedules?.size?:0, it?.size?:0)
            viewModel.schedulesAdapter.notifyItemRangeChanged(0, size)
        }
        with(binding){
            rvSchedules.adapter = viewModel.schedulesAdapter
            rvSchedules.layoutManager = LinearLayoutManager(
                this@SchedulesFragment.context,LinearLayoutManager.VERTICAL,false
            )
            btnAddOuterSchedule.setOnClickListener {
                findNavController().navigate(R.id.action_nav_schedules_to_nav_add_outer_schedule)
            }
            tbShowMySchedules.setOnCheckedChangeListener { buttonView, isChecked ->
                rvSchedules.recycledViewPool.clear()
                viewModel.schedulesAdapter.showMySchedules = isChecked
            }
            btnSetInnerScheduleToInstructor.setOnClickListener {
                findNavController().navigate(
                    R.id.action_nav_schedules_to_nav_add_inner_schedule,
                )
            }
            when(App.controller.loginResponse.value!!.role){
                UserRoles.admin -> {
                    llSchedulesForAdmin.visibility = View.VISIBLE
                    llSchedulesForInstructors.visibility = View.GONE
                    llSchedulesForInstructorsAndStudents.visibility = View.GONE
                }
                UserRoles.instructor -> {
                    llSchedulesForAdmin.visibility = View.GONE
                    llSchedulesForInstructors.visibility = View.VISIBLE
                    llSchedulesForInstructorsAndStudents.visibility = View.VISIBLE
                }
                UserRoles.student -> {
                    llSchedulesForAdmin.visibility = View.GONE
                    llSchedulesForInstructors.visibility = View.GONE
                    llSchedulesForInstructorsAndStudents.visibility = View.VISIBLE
                }
            }
            return root
        }
    }

    override fun onDestroy() {
        super.onDestroy()
        App.schedules.removeObservers(this)
        App.mySchedules.removeObservers(this)
    }
}