import csv
import os
import pyodbc
import sys

conn = pyodbc.connect('Driver={ODBC Driver 17 for SQL Server};'
                      'Server=tcp:polsl.database.windows.net,1433;'
                      'Database=polslDB;'
                      'Persist Security Info=False;'
                      'UID=wojtololog;'
                      'PWD=Admin1!!;'
                      'MultipleActiveResultSets=False;'
                      'Encrypt=Yes;'
                      'TrustServerCertificate=No;'
                      'Connection Timeout=30;'
                      )
cursor = conn.cursor()
cursor.execute('select * from ' + sys.argv[1])

if not os.path.exists('../exports/'):
    os.makedirs('../exports/')

with open('../exports/' + sys.argv[1] + '.csv', "w", newline='') as csv_file:
    csv_writer = csv.writer(csv_file)
    csv_writer.writerow([i[0] for i in cursor.description])
    csv_writer.writerows(cursor)
