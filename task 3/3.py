def get_minimum_transitions_number(
    employees_number,
    first_departure_time,
    floor_numbers,
    first_employee,
):
    minimum_distance_from_edge = min(
        floor_numbers[first_employee - 1] - floor_numbers[0],
        floor_numbers[employees_number - 1] - floor_numbers[first_employee - 1],
    )

    return (
        floor_numbers[employees_number - 1] - floor_numbers[0]
        if minimum_distance_from_edge <= first_departure_time
        else floor_numbers[employees_number - 1]
        - floor_numbers[0]
        + minimum_distance_from_edge
    )


def read_input():
    employees_number, first_departure_time = tuple(map(int, input().strip().split()))
    floor_numbers = list(map(int, input().strip().split()))
    first_employee = int(input().strip())
    return (
        employees_number,
        first_departure_time,
        floor_numbers,
        first_employee,
    )


def main():
    print(get_minimum_transitions_number(*read_input()))


if __name__ == "__main__":
    main()