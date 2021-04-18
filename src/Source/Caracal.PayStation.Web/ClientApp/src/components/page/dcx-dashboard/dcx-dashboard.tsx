import {Component, h, Element, Prop} from '@stencil/core';
import Chart from 'chart.js/auto';

@Component({
  tag: 'dcx-dashboard',
  styleUrl: 'dcx-dashboard.scss',
  shadow: false,
})
export class DcxDashboard {

  @Element() el: HTMLElement;
  @Prop() type: string = 'line';


  componentDidLoad() {
    const canvas: any = this.el.querySelector('#myChart');

    var myCtx = canvas.getContext('2d');
    new Chart(myCtx, {
      type: this.type,
      data: {
        labels: ['Red', 'Blue', 'Yellow', 'Green', 'Purple', 'Orange'],
        datasets: [{
          label: '# of Votes',
          data: [12, 19, 3, 5, 2, 3],
          backgroundColor: [
            'rgba(255, 99, 132, 0.2)',
            'rgba(54, 162, 235, 0.2)',
            'rgba(255, 206, 86, 0.2)',
            'rgba(75, 192, 192, 0.2)',
            'rgba(153, 102, 255, 0.2)',
            'rgba(255, 159, 64, 0.2)'
          ],
          borderColor: [
            'rgba(255, 99, 132, 1)',
            'rgba(54, 162, 235, 1)',
            'rgba(255, 206, 86, 1)',
            'rgba(75, 192, 192, 1)',
            'rgba(153, 102, 255, 1)',
            'rgba(255, 159, 64, 1)'
          ],
          borderWidth: 1
        }]
      },
      options: {
        scales: {
          y: {
            beginAtZero: true
          }
        }
      }
    });
  }


  render() {
    return <canvas id="myChart" width="100px" height="100px"></canvas>;
  }
}
