import { Config } from '@stencil/core';
import { sass } from '@stencil/sass';

// https://stenciljs.com/docs/config

export const config: Config = {
  globalStyle: 'src/global/app.scss',
  globalScript: 'src/global/app.ts',
  plugins: [
    sass()
  ],
  taskQueue: 'async',
  outputTargets: [
    {
      type: 'www',
      // comment the following line to disable service workers in production
      serviceWorker: null,
      empty: true,
      baseUrl: 'https://myapp.local/',
      copy: [
        { src: '../node_modules/caracal_apex/dist', dest: 'lib/caracal_apex' },
        { src: '../node_modules/chart.js/dist', dest: 'lib/chart_js' }
      ]
    },
  ],
};
