import sys

from matplotlib import pyplot

x_legend_table = ['Price [zl]', 'Distance [m]', 'Size [mg]', 'Size [ml]', 'Size [szt]']
values_table = [0, 1, 1, 0]

if __name__ == '__main__':
    cheap_table = sys.argv[1:5]
    semi_cheap_table = sys.argv[5:9]
    medium_table = sys.argv[9:13]
    semi_expensive_table = sys.argv[13:17]
    expensive_table = sys.argv[17:21]
    x_legend = ''
    if sys.argv[21] == 'c':
        x_legend = x_legend_table[0]
    elif sys.argv[21] == 'd':
        x_legend = x_legend_table[1]
    elif sys.argv[21] == 'mg':
        x_legend = x_legend_table[2]
    elif sys.argv[21] == 'ml':
        x_legend = x_legend_table[3]
    elif sys.argv[21] == 'szt':
        x_legend = x_legend_table[4]
    else:
        exit()
    pyplot.plot(cheap_table, values_table, label='very low')
    pyplot.plot(semi_cheap_table, values_table, label='low')
    pyplot.plot(medium_table, values_table, label='medium')
    pyplot.plot(semi_expensive_table, values_table, label='high')
    pyplot.plot(expensive_table, values_table, label='very high')
    pyplot.legend(loc='upper left')
    pyplot.ylim([0, 1.6])
    pyplot.xlabel(x_legend)
    pyplot.ylabel('Degrees of membership [SF]')
    pyplot.title(sys.argv[22])
    pyplot.show()
