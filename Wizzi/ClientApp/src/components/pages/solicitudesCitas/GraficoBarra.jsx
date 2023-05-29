import React from 'react'
import { Pie,Bar } from 'react-chartjs-2';
import ChartDataLabels from 'chartjs-plugin-datalabels';
const GraficoBarra = (props) => {
    const {datos, labels} = props
    const styles = {
        pieContainer: {
          width: "700px",
          height: "200px",
          
          
        },
       
      };
      const data = {
        labels: labels,
        datasets: [
          {
            label: '# solicitudes',
            data: datos,
            backgroundColor: [
              'rgba(255, 99, 132, 0.2)',
              'rgba(54, 162, 235, 0.2)',
              'rgba(255, 206, 86, 0.2)',
              'rgba(75, 192, 192, 0.2)',
              'rgba(153, 102, 255, 0.2)',
              'rgba(255, 159, 64, 0.2)',
            ],
            borderColor: [
              'rgba(255, 99, 132, 1)',
              'rgba(54, 162, 235, 1)',
              'rgba(255, 206, 86, 1)',
              'rgba(75, 192, 192, 1)',
              'rgba(153, 102, 255, 1)',
              'rgba(255, 159, 64, 1)',
            ],
            borderWidth: 1,
            datalabels: {
                display: true,
                color: "black",
                align: "end",
                anchor: "end",
                // color: function(context) {
                //   return context.dataset.backgroundColor;
                // },
                font: function(context) {
                  var w = context.chart.width;
                  return {
                    size: w < 512 ? 12 : 14,
                    // weight: 'bold',
                  };
                }
              }
          },
        ],
      };
   
        const options = {
           
              indexAxis: 'y',
              elements: {
                bar: {
                  borderWidth: 2,
                },
              },
              responsive: true,
              plugins: {
                legend: {
                  display: false,
                  position: 'right',

                },
                title: {
                  display: false,
                  text: 'Resumen Solicitudes',
                },
              },
            };
            
    
    return (
        <>
            <div style={styles.pieContainer}>
            <Bar
                data={data}
                options={options}
                width={1500}
                height={400}
                plugins={[ChartDataLabels]}
            />
            </div>
        </>
    )
}
export default GraficoBarra
