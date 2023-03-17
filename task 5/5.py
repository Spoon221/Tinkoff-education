def get_answer(l, r):
    len_l, len_r = len(str(l)), len(str(r))
    counter = (len_r - len_l - 1) * 9

    i = 9
    while int(str(i) * len_l) >= l and i != 0:
        counter += 1
        i -= 1

    i = 1
    while int(str(i) * len_r) <= r and i != 10:
        counter += 1
        i += 1

    return counter


def read_input():
    return tuple(map(int, input().strip().split()))


def main():
    print(get_answer(*read_input()))


if __name__ == "__main__":
    main()