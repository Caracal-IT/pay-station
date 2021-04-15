import { Component, Prop, h } from '@stencil/core';
import {Context} from 'caracal_polaris/dist/types/model/context.model';

@Component({
  tag: 'apx-grid',
  styleUrl: 'apx-grid.scss',
  shadow: true,
})
export class ApexGrid {
  @Prop() caption: string;
  @Prop() ctx: Context;

  onStatusInput(evt) {
    const updateStatus = [
      { "withdrawalId": 2, "status": evt.target.value }
    ]

    this.ctx.model.setValue("withdrawals.status", updateStatus);
  }

  render() {
    const model = this.ctx.model.getValue("withdrawals.searchResult");

    return [
      <div><h2>{this.caption}</h2></div>,
      <div><span>Status for 2</span><input onInput={this.onStatusInput.bind(this)} /></div>,
      <table>
        <thead>
          <tr>
            <th>Id</th>
            <th>Account</th>
            <th>Amount</th>
            <th>Status</th>
          </tr>
        </thead>
        <tbody>
          {model ? model.map(i => <tr><td>{i.id}</td><td>{i.account}</td><td>{i.amount}</td><td>{i.status}</td></tr>): null}
        </tbody>
      </table>
    ];
  }
}
