def get_cuts_number(N):
    power_of_two = 0
    while N > 2**power_of_two:
        power_of_two += 1
    return power_of_two


def read_input():
    return int(input().strip())


def main():
    print(get_cuts_number(read_input()))


if __name__ == "__main__":
    main()