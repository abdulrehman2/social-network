import { Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { MenuItem } from 'primeng/api';
import { User } from 'src/app/models/user';
import { UserService } from 'src/app/services/user-service';
import {
  ChartComponent,
  ApexAxisChartSeries,
  ApexChart,
  ApexFill,
  ApexTooltip,
  ApexXAxis,
  ApexLegend,
  ApexDataLabels,
  ApexTitleSubtitle,
  ApexPlotOptions,
  ApexYAxis,
} from 'ng-apexcharts';

export type ChartOptions = {
  series: ApexAxisChartSeries;
  chart: ApexChart;
  xaxis: ApexXAxis;
  markers: any; //ApexMarkers;
  stroke: any; //ApexStroke;
  yaxis: ApexYAxis | ApexYAxis[];
  plotOptions: ApexPlotOptions;
  dataLabels: ApexDataLabels;
  colors: string[];
  labels: string[] | number[];
  title: ApexTitleSubtitle;
  subtitle: ApexTitleSubtitle;
  legend: ApexLegend;
  fill: ApexFill;
  tooltip: ApexTooltip;
};

const sparkLineData = [
  47, 45, 54, 38, 56, 24, 65, 31, 37, 39, 62, 51, 35, 41, 35, 27, 93, 53, 61,
  27, 54, 43, 19, 46,
];

@Component({
  selector: 'app-side-bar',
  templateUrl: './side-bar.component.html',
  styleUrls: ['./side-bar.component.scss'],
})
export class SideBarComponent implements OnInit {
  pagesMenu: MenuItem[];
  user: User;
  @ViewChild('chart') chart: ChartComponent;
  public chartOptions: Partial<ChartOptions>;
  public chartAreaSparkline3Options: Partial<ChartOptions>;
  public commonAreaSparlineOptions: Partial<ChartOptions> = {
    chart: {
      type: 'area',
      height: 100,
      sparkline: {
        enabled: true,
      },
    },
    stroke: {
      curve: 'straight',
    },
    fill: {
      opacity: 0.3,
    },
    yaxis: {
      min: 0,
    },
  };

  constructor(private router: Router, private userService: UserService) {
    // window.Apex = {
    //   stroke: {
    //     width: 3
    //   },
    //   markers: {
    //     size: 0
    //   },
    //   tooltip: {
    //     fixed: {
    //       enabled: true
    //     }
    //   }
    // };

    this.chartAreaSparkline3Options = {
      series: [
        {
          name: 'chart-big-sparkline',
          data: sparkLineData,
        },
      ],
      title: {
        text: '$135,965',
        offsetX: 40,
        style: {
          fontSize: '16px',
        },
      },
      subtitle: {
        // text: 'Return',
        // offsetX: 0,
        // style: {
        //   fontSize: '14px',
        // },
      },
    };
  }

  ngOnInit(): void {
    this.user = this.userService.getUserModel();
    this.pagesMenu = [
      {
        label: 'Markets',
        items: [
          {
            label: 'Equity',
            icon: ' text-700 pi pi-chart-line',
            command: () => {},
          },
          {
            label: 'ETFs',
            icon: ' text-700 pi pi-euro',
            command: () => {},
          },
          {
            label: 'Crypto',
            icon: ' text-700 pi pi-key',
            command: () => {},
          },
        ],
      },
      {
        label: 'Investments',
        items: [
          {
            label: 'Portfolio',
            icon: 'text-green-600 pi pi-dollar',
            command: () => {},
          },
          {
            label: 'My Watchlist',
            icon: 'text-green-600 pi pi-bars',
            command: () => {},
          },
        ],
      },
      {
        label: 'Social',
        items: [
          {
            label: 'Friends',
            icon: 'text-cyan-600 pi pi-users',
            command: () => {
              this.router.navigate(['/home/friends']);
            },
          },
          {
            label: 'Pending Requests',
            icon: 'text-cyan-600 pi pi-info-circle',
            command: () => {
              this.router.navigate(['/home/pendingrequests']);
            },
          },
        ],
      },

      {
        label: 'Settings',
        items: [
          {
            label: 'Profile',
            icon: 'text-red-600 pi pi-user',
            command: () => {
              this.router.navigate(['/home/user-profile']);
            },
          },
          {
            label: 'Privacy',
            icon: 'text-red-600 pi pi-ban',
            command: () => {
              this.router.navigate(['/home/privacy-settings']);
            },
          },
          {
            label: 'Sign out',
            icon: 'text-red-600 pi pi-sign-out',
            command: () => {
              this.router.navigate(['/login']);
            },
          },
        ],
      },
    ];
  }
}
