def get_answer(students):
    i = 0
    even_stack = []
    odd_stack = []
    while i < len(students):
        if (i + students[i]) % 2 == 0:
            if students[i] % 2 == 0:
                even_stack.append(i)
            else:
                odd_stack.append(i)
        i += 1

    if len(even_stack) == 1 and len(odd_stack) == 1:
        return even_stack[0] + 1, odd_stack[0] + 1
    elif len(even_stack) == 0 and len(odd_stack) == 0 and len(students) >= 3:
        return 1, 3
    else:
        return -1, -1


def read_input():
    students_len = int(input().strip())
    students = list(map(int, input().strip().split()))
    return students_len, students


def main():
    students_len, students = read_input()
    print(*get_answer(students))


if __name__ == "__main__":
    main()