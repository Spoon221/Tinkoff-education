def get_internet_costs(A, B, C, D):
    return A + (D - B) * C if D - B > 0 else A


def read_input():
    return tuple(map(int, input().strip().split()))


def main():
    A, B, C, D = read_input()
    print(get_internet_costs(A, B, C, D))


if __name__ == "__main__":
    main()