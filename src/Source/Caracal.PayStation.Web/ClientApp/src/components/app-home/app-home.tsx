import { Component, h } from '@stencil/core';

@Component({
  tag: 'app-home',
  styleUrl: 'app-home.css',
  shadow: true,
})
export class AppHome {
  process = {
    "name" : "demo",
    "activities": [
      {
        "name": "start",
        "type": "page-activity",
        "controls": [
          {"tag" : "h1", "innerHTML": "Polaris" },
          {"tag" : "span", "innerHTML": "Welcome to polaris workflow" }
        ]
      }
    ]
  };

  render() {
    return (
      <div class="app-home">
        <p>
          Welcome to the Stencil App Starter. You can use this starter to build entire apps all with web components using Stencil! Check out our docs on{' '}
          <a href="https://stenciljs.com">stenciljs.com</a> to get started.
        </p>

        <polaris-workflow url="assets/workflow/data/settings.json" process="login">

        </polaris-workflow>

        <stencil-route-link url="/profile/stencil">
          <button>Profile page</button>
        </stencil-route-link>
      </div>
    );
  }
}
