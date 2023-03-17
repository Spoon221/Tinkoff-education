def is_circular(students):
    track = {0}
    gifted_student = students[0]
    while gifted_student != 0 and gifted_student not in track:
        track.add(gifted_student)
        gifted_student = students[gifted_student]
    if gifted_student == 0:
        return len(track) == len(students)
    return False


def redirect(students, from_whom, to_whom):
    redirected_students = students.copy()
    redirected_students[from_whom] = to_whom
    return redirected_students


def get_answer(students):
    students = convert(students)
    students_len = len(students)

    if len(set(students)) == students_len - 1:
        stack = [0 for i in range(students_len)]
        for student in students:
            stack[student] += 1

        double_gifted_student = stack.index(2)
        no_gifted_student = stack.index(0)

        transactions = []
        i = 0
        while i <= students_len - 1:
            if students[i] == double_gifted_student:
                transactions.append((i, no_gifted_student))
            i += 1

        for transaction in transactions:
            if is_circular(
                redirect(
                    students,
                    transaction[0],
                    transaction[1],
                ),
            ):
                return transaction[0] + 1, transaction[1] + 1

    return (-1, -1)


def convert(x):
    return [i - 1 for i in x]


def read_input():
    students_len = int(input().strip())
    students = list(map(int, input().strip().split()))
    return students_len, students


def main():
    students_len, students = read_input()
    print(*get_answer(students))


if __name__ == "__main__":
    assert is_circular(convert([1, 3, 1])) == False
    assert is_circular(convert([2, 3, 1])) == True
    assert is_circular(convert([2, 1, 4, 5, 3])) == False
    assert is_circular(convert([2, 3, 2])) == False

    assert get_answer([1, 2, 3]) == (-1, -1)
    assert get_answer([1, 3, 1]) == (1, 2)
    assert get_answer([2, 1, 2, 3, 4]) == (1, 5)
    assert get_answer([2, 1, 4, 5, 3]) == (-1, -1)
    assert get_answer([2, 2]) == (2, 1)

    assert redirect([1, 1], 1, 0) == [1, 0]
    assert redirect([1, 0, 1, 2, 3], 0, 4) == [4, 0, 1, 2, 3]
    main()