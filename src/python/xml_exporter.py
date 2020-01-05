import csv
import sqlite3


connection = sqlite3.connect()
cursor = connection.cursor()
cursor.execute('select * from *;')
with open("out.csv", "w", newline='') as csv_file:
    csv_writer = csv.writer(csv_file)
    csv_writer.writerow([i[0] for i in cursor.description])
    csv_writer.writerows(cursor)