﻿using DataUtility.DataStructures.Metrics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataUtility.DataStructures.VectorDataConversion
{
    public class EpsilonKNN : IVectorConversion
    {
        public double Radius { get; set; }
        public int K { get; set; }

        public EpsilonKNN(double radius, int k)
        {
            this.Radius = radius;
            this.K = k;
        }
       
        public Network ConvertToNetwork(DataFrame vectorData, IMetric metric, IColumn idColumn)
        {
            Network result = new Network(vectorData.DataCount);
            Matrix<double> kernelMatrix = metric.GetMetricMatrix(vectorData);


            for (int i = 0; i < kernelMatrix.Rows; i++)
            {
                Dictionary<int, double> dict = new Dictionary<int, double>();

                for (int j = i; j < kernelMatrix.Cols; j++)
                {
                    if (i != j)
                    {
                        dict[j] = kernelMatrix[i, j];
                    }

                }

                var orderedDict = dict.OrderByDescending(key => key.Value);

                int edgeCount = 0;

                foreach (KeyValuePair<int, double> pair in orderedDict)
                {
                    if (edgeCount >= this.K && pair.Value < this.Radius)
                        break;

                    result.AddIndirectedEdge(idColumn[i].ToString(), idColumn[pair.Key].ToString(), 1);
                    edgeCount++;
                }
            }

            return result;
        }
    }
}