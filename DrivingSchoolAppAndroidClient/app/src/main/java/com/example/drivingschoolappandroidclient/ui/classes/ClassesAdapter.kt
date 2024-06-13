package com.example.drivingschoolappandroidclient.ui.classes

import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.lifecycle.MutableLiveData
import androidx.recyclerview.widget.RecyclerView
import com.example.drivingschoolappandroidclient.App
import com.example.drivingschoolappandroidclient.databinding.ClassItemBinding
import com.example.drivingschoolappandroidclient.models.models.Class
import com.example.drivingschoolappandroidclient.models.models.UserRoles

class ClassesAdapter : RecyclerView.Adapter<ClassesAdapter.ClassViewHolder>() {
    private var mShowMyClasses: Boolean = false
    private var mShowClassesOfMyInstructor: Boolean = false
    var showMyClasses
        get() = mShowMyClasses
        set(value) {
            if(mShowClassesOfMyInstructor)
                showClassesOfMyInstructor = false
            mShowMyClasses = value
            notifyDataSetChanged()
        }
    var showClassesOfMyInstructor
        get() = mShowClassesOfMyInstructor
        set(value) {
            if(mShowMyClasses)
                showMyClasses = false
            mShowClassesOfMyInstructor = value
            notifyDataSetChanged()
        }
    val classes : List<Class>?
        get() {
            return if(mShowMyClasses)
                App.myClasses.value
            else if(mShowClassesOfMyInstructor)
                App.classesOfMyInstructor.value
            else
                App.classes.value
        }
    class ClassViewHolder(val binding: ClassItemBinding): RecyclerView.ViewHolder(binding.root)

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int) =
        ClassViewHolder(ClassItemBinding.inflate(LayoutInflater.from(parent.context),parent,false))

    override fun getItemCount(): Int = classes?.size ?: 0
    var selectedPosition: MutableLiveData<Int?> = MutableLiveData(null)
    val selected: Class? get() = selectedPosition.value?.let { classes?.get(it) }
    override fun onBindViewHolder(holder: ClassViewHolder, position: Int) {
        val element = classes!![position]
        with(holder.binding){
            if(App.controller.loginResponse.value!!.role == UserRoles.admin)
                tvClassSelected.visibility = View.GONE
            else{
                tvClassSelected.visibility = View.VISIBLE
                root.setOnClickListener{
                    if(selectedPosition.value==position){
                        selectedPosition.value = null
                        notifyItemChanged(position)
                        return@setOnClickListener
                    }
                    selectedPosition.value?.let{ notifyItemChanged(it)}
                    notifyItemChanged(position)
                    selectedPosition.value = position
                }
                val sel =
                    if (selectedPosition.value == position) "●"
                    else "○"
                tvClassSelected.text = sel
            }
            tvClassInstructor.text = element.instructor.toString()
            tvClassDate.text = App.dateToString(element.date)
            val period = "${element.startTime} - ${element.endTime}"
            tvClassPeriod.text = period
            tvClassStatus.text = element.classStatus.name
            tvClassStudent.text = element.student?.toString() ?: "не назначен"
        }
    }

}