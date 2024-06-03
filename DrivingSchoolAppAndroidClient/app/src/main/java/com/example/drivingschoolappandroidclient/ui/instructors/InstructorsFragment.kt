package com.example.drivingschoolappandroidclient.ui.instructors

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import androidx.lifecycle.ViewModelProvider
import androidx.recyclerview.widget.LinearLayoutManager
import com.example.drivingschoolappandroidclient.App
import com.example.drivingschoolappandroidclient.models.models.UserRoles
import com.example.drivingschoolappandroidclient.databinding.FragmentInstructorsBinding

class InstructorsFragment : Fragment() {

    private lateinit var binding: FragmentInstructorsBinding
    private lateinit var viewModel: InstructorsViewModel

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        binding = FragmentInstructorsBinding.inflate(layoutInflater)
        viewModel = ViewModelProvider(this).get(InstructorsViewModel::class.java)
        App.instructors.observe(viewLifecycleOwner){
            val size = maxOf(viewModel.instructorsAdapter.instructors.size, it?.size?:0)
            viewModel.instructorsAdapter.notifyItemRangeChanged(0,size)
        }
        App.myInstructor.observe(viewLifecycleOwner){
            val size = maxOf(viewModel.instructorsAdapter.instructors.size, 1)
            viewModel.instructorsAdapter.notifyItemRangeChanged(0,size)
        }
        with(binding){
            rvInstructors.adapter = viewModel.instructorsAdapter
            rvInstructors.layoutManager = LinearLayoutManager(
                this@InstructorsFragment.context,LinearLayoutManager.VERTICAL,false
            )
            if(App.controller.loginResponse.value?.role == UserRoles.student)
                llInstructorsForStudent.visibility = View.VISIBLE
            else
                llInstructorsForStudent.visibility = View.GONE
            tbShowMyInstructor.setOnCheckedChangeListener { buttonView, isChecked ->
                rvInstructors.recycledViewPool.clear()
                viewModel.instructorsAdapter.showMyInstructor = isChecked
            }
            return root
        }
    }

    override fun onDestroy() {
        super.onDestroy()
        App.myInstructor.removeObservers(this)
        App.instructors.removeObservers(this)
    }
}