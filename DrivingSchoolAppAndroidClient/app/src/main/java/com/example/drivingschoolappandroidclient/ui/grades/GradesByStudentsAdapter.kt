package com.example.drivingschoolappandroidclient.ui.grades

import android.view.LayoutInflater
import android.view.ViewGroup
import androidx.recyclerview.widget.RecyclerView
import com.example.drivingschoolappandroidclient.App
import com.example.drivingschoolappandroidclient.databinding.GradeItemBinding

class GradesByStudentsAdapter : RecyclerView.Adapter<GradesByStudentsAdapter.GradesViewHolder>() {
    class GradesViewHolder(val binding: GradeItemBinding): RecyclerView.ViewHolder(binding.root)

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int) = GradesViewHolder(
        GradeItemBinding.inflate(LayoutInflater.from(parent.context),parent,false)
    )

    override fun getItemCount(): Int = App.gradesByStudentsToInstructor.value?.size ?: 0

    override fun onBindViewHolder(holder: GradesViewHolder, position: Int) {
        val element = App.gradesByStudentsToInstructor.value!![position]
        with (holder.binding){
            tvGradeClass.text = element._class.toString()
            tvGradeStudent.text = element._class.student.toString()
            tvGradeInstructor.text = element._class.instructor.toString()
            tvGradeGrade.text = element.gradeEnum.name
            tvGradeComment.text = element.comment ?: ""
        }
    }
}