import sys

from matplotlib import pyplot


if __name__ == '__main__':
    cheap_table = sys.argv[0:4]
    medium_table = sys.argv[4:8]
    expensive_table = sys.argv[8:12]
    values_table = [0, 1, 1, 0]

    pyplot.plot(cheap_table, values_table)
    pyplot.plot(medium_table, values_table)
    pyplot.plot(expensive_table, values_table)
    pyplot.show()
