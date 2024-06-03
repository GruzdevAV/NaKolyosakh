package com.example.drivingschoolappandroidclient.models.models

/**
 * Оценки от инструкторов ученикам
 */
enum class GradesByInstructorsToStudents(val value: Byte) {
    Прогул(1),
    Удовлетворительно(2),
    Хорошо(3),
    Отлично(4);
    constructor(str: String) : this(str.toByte())
    companion object{
        fun ofValue(value: Byte) = GradesByInstructorsToStudents.values().first { it.value == value }
    }
}

/**
 * Оценки от учеников инструкторам
 */
enum class GradesByStudentsToInstructors(val value: Byte) {
    Ужасно(1),
    Плохо(2),
    Удовлетворительно(3),
    Хорошо(4),
    Отлично(5);
    constructor(str: String) : this(str.toByte())
    companion object{
        fun ofValue(value: Byte) = GradesByStudentsToInstructors.values().first { it.value == value }
    }
}

/**
 * Состояния занятий
 */
enum class ClassStatus(val value: Byte) {
    Предстоит(0),
    Завершено(1),
    Отменено(2);
    constructor(str: String) : this(str.toByte())
    companion object{
        fun ofValue(value: Byte) = ClassStatus.values().first { it.value == value }
    }
}

enum class ClassType(val value: Byte) {
    Inner(0),
    Outer(1);
    constructor(str: String) : this(str.toByte())
    companion object{
        fun ofValue(value: Byte) = ClassType.values().first { it.value == value }
    }
}
