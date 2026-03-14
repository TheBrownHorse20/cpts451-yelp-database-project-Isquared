import json


def cleanStr4SQL(s):
    return s.replace("'","''").replace("\n"," ")

def getAttributes(attributes):
    L = []
    for (attribute, value) in list(attributes.items()):
        if isinstance(value, dict):
            L += getAttributes(value)
        else:
            L.append((attribute,value))
    return L

def parseBusinessData():
    print("Parsing businesses...")
    #read the JSON file
    with open('./yelp_business.JSON','r') as f:
        outfile =  open('./yelp_business.txt', 'w')
        line = f.readline()
        count_line = 0
        #read each JSON abject and extract data
        while line:
            data = json.loads(line)
            business = data['business_id'] #business id
            business_str =  "'" + cleanStr4SQL(data['name']) + "'," + \
                            "'" + cleanStr4SQL(data['address']) + "'," + \
                            "'" + cleanStr4SQL(data['city']) + "'," +  \
                            "'" + data['state'] + "'," + \
                            "'" + data['postal_code'] + "'," +  \
                            str(data['latitude']) + "," +  \
                            str(data['longitude']) + "," + \
                            str(data['stars']) + "," + \
                            str(data['review_count']) + "," + \
                            str(data['is_open'])
            outfile.write(business_str + '\n')

            # process business categories
            for category in data['categories']:
                category_str = "'" + business + "','" + category + "'"
                outfile.write(category_str + '\n')

            # process business hours
            for (day,hours) in data['hours'].items():
                hours_str = "'" + business + "','" + str(day) + "','" + str(hours.split('-')[0]) + "','" + str(hours.split('-')[1]) + "'"
                outfile.write( hours_str +'\n')

            #process business attributes
            for (attr,value) in getAttributes(data['attributes']):
                attr_str = "'" + business + "','" + str(attr) + "','" + str(value)  + "'"
                outfile.write(attr_str +'\n')

            line = f.readline()
            count_line +=1
    print(count_line)
    outfile.close()
    f.close()

def parseCheckInData():
    print("Parsing checkins...")
    with open('./yelp_checkin.JSON', 'r') as f:
        outfile = open('./yelp_checkin.txt', 'w')
        line = f.readline()
        count_line = 0

        while line:
            data = json.loads(line)
            business = data['business_id']

            for day, times in data['time'].items():
                for hour, count in times.items():
                    checkin_str = "'" + business + "','" + str(day) + "','" + str(hour) + "'," + str(count)
                    outfile.write(checkin_str + '\n')

            line = f.readline()
            count_line += 1

        print(count_line)
        outfile.close()

def parseReviewData():
    print("Parsing reviews...")
    with open('./yelp_review.JSON', 'r') as f:
        outfile = open('./yelp_review.txt', 'w')
        line = f.readline()
        count_line = 0

        while line:
            data = json.loads(line)

            review_str = "'" + data['business_id'] + "'," + \
                         "'" + data['user_id'] + "'," + \
                         "'" + data['review_id'] + "'," + \
                         str(data['stars']) + "," + \
                         "'" + data['date'] + "'," + \
                         "'" + cleanStr4SQL(data['text']) + "'," + \
                         str(data['useful']) + "," + \
                         str(data['funny']) + "," + \
                         str(data['cool'])

            outfile.write(review_str + '\n')

            line = f.readline()
            count_line += 1

        print(count_line)
        outfile.close()
        f.close()

def parseUserData():
    print("Parsing users...")
    with open('./yelp_user.JSON', 'r') as f:
        outfile = open('./yelp_user.txt', 'w')
        line = f.readline()
        count_line = 0

        while line:
            data = json.loads(line)
            user = data['user_id']

            user_str = "'" + user + "'," + \
                       "'" + cleanStr4SQL(data['name']) + "'," + \
                       str(data['review_count']) + "," + \
                       str(data['useful']) + "," + \
                       str(data['funny']) + "," + \
                       str(data['cool']) + "," + \
                       str(data['fans']) + "," + \
                       str(data['average_stars']) + "," + \
                       "'" + data['yelping_since'] + "'," + \
                       str(data['compliment_cool']) + "," + \
                       str(data['compliment_cute']) + "," + \
                       str(data['compliment_funny']) + "," + \
                       str(data['compliment_hot']) + "," + \
                       str(data['compliment_list']) + "," + \
                       str(data['compliment_more']) + "," + \
                       str(data['compliment_note']) + "," + \
                       str(data['compliment_photos']) + "," + \
                       str(data['compliment_plain']) + "," + \
                       str(data['compliment_profile']) + "," + \
                       str(data['compliment_writer'])

            outfile.write(user_str + '\n')

            for year in data['elite']:
                elite_str = "'" + user + "','" + str(year) + "'"
                outfile.write(elite_str + '\n')

            for friend in data['friends']:
                friend_str = "'" + user + "','" + friend + "'"
                outfile.write(friend_str + '\n')

            line = f.readline()
            count_line += 1

        print(count_line)
        outfile.close()
        f.close()


parseBusinessData()
parseCheckInData()
parseReviewData()
parseUserData()
