import numpy as np
import matplotlib.pyplot as plt
import pandas as pd
import csv 

# Importing dataset
dataset = pd.read_csv('/Users/arturo/dev/Stream/AS_Regression/regression0425-b.csv')
X = dataset.iloc[:, :-1].values
y = dataset.iloc[:, -1].values

#Enconding categorical Data
from sklearn.compose import ColumnTransformer
#from sklearn.preprocessing import OnesHotEncoder
#ct = ColumnTransformer(transformers=[('encoder', OneHotEncoder(), [3])], remainder='passthrough')
#X = np.array(ct.fit_transform(X))

#Splitting the dataset into the Training set and Test set
from sklearn.model_selection import train_test_split
X_train, X_test, y_train, y_test = train_test_split(X, y, test_size = 0.2, random_state = 0)

#Training the Multiple Linear Regression model on the Training set
from sklearn.linear_model import LinearRegression
regressor = LinearRegression()
regressor.fit(X_train, y_train)

#Predicting the Test set results
y_pred = regressor.predict(X_test)
np.set_printoptions(precision=2)
# for reshape the last 1 means horizontal concatenation, 0 means vertical
result = (np.concatenate((y_pred.reshape(len(y_pred),1), y_test.reshape(len(y_test),1)),1))

with open("/Users/arturo/dev/Stream/AS_Regression/output-0425-b.csv", "w") as f:
    writer = csv.writer(f)
    writer.writerows(result)