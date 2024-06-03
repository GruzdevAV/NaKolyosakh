package com.example.drivingschoolappandroidclient.ui.schedules

import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.recyclerview.widget.RecyclerView
import com.example.drivingschoolappandroidclient.App
import com.example.drivingschoolappandroidclient.databinding.ScheduleItemBinding
import com.example.drivingschoolappandroidclient.models.models.InnerScheduleOfInstructor

class SchedulesAdapter : RecyclerView.Adapter<SchedulesAdapter.SchedulesViewHolder>() {
    val schedules : List<InnerScheduleOfInstructor>?
        get() {
            return if(mShowMySchedules)
                App.mySchedules.value
            else App.schedules.value
        }
    private var mShowMySchedules = false
    var showMySchedules
        get() = mShowMySchedules
        set(value) {
            mShowMySchedules = value
            notifyDataSetChanged()
        }
    class SchedulesViewHolder(val binding: ScheduleItemBinding) : RecyclerView.ViewHolder(binding.root)

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int) = SchedulesViewHolder(
        ScheduleItemBinding.inflate(LayoutInflater.from(parent.context),parent,false)
    )

    override fun getItemCount(): Int = schedules?.size ?: 0

    override fun onBindViewHolder(holder: SchedulesViewHolder, position: Int) {
        val element = schedules!![position]
        with (holder.binding){
            val instructor = "${element.instructor.lastName} ${element.instructor.firstName} ${element.instructor.patronymic?:""}"
            tvSchedulesInstructor.text = instructor
            tvSchedulesDate .text = element.dayOfWorkJson
            if (element.IsOuterSchedule)
                tvSchedulesIsOuter.text = "(внешнее)"
            else tvSchedulesIsOuter.visibility = View.GONE
            tvSchedulesClasses.visibility = View.GONE
        }
    }
}