import { Component, Prop, h } from '@stencil/core';
import {Context} from 'caracal_polaris/dist/types/model/context.model';

@Component({
  tag: 'dcx-grid',
  styleUrl: 'dcx-grid.scss',
  shadow: true,
})
export class DcxGrid {
  @Prop() headers: any;
  @Prop() caption: string;
  @Prop() ctx: Context;
  @Prop({mutable: true, reflect: true}) value: Array<any>;

  onSelected(item, evt) {
    item.selected = evt.detail;
    this.value = [...this.value];
  }

  onSelectedRow(item) {
    item.selected = !item.selected||false;
    this.value = [...this.value];
  }

  componentWillRender() {
    if(this.headers || !this.value || this.value.length < 1) return;

    this.headers = Object.keys(this.value[0])
                         .map(this.createHeader);
  }

  createHeader(header) {
    return {
      key: header,
      text: (header).replace(/([A-Z])/g, ' $1').trim()
    };
  }

  renderHeader() {
    return <tr>
      <th/>
      {this.headers.map(head => <th>{head.text}<resize-handle/></th>)}
    </tr>;
  }

  renderRow(i) {
    return <tr onClick={this.onSelectedRow.bind(this, i)}  class={i.selected?'selected':''}>
      <td><apx-check value={i.selected} onCheckChanged={this.onSelected.bind(this, i)}/></td>
      {this.headers.map(head => <td>{i[head.key]}<resize-handle/></td>)}
    </tr>;
  }

  renderCaption() {
    if(this.caption == '')
      return null;

    return <div><h2>{this.caption}</h2></div>;
  }

  render() {
    if(!this.value || this.value.length == 0)
      return <div id="tableDiv"><h2>No Results</h2></div>;

    return [
      this.renderCaption(),
      <div id="tableDiv">
        <table>
          <thead>{this.renderHeader()}</thead>
          <tbody>{this.value.map(this.renderRow.bind(this))}</tbody>
        </table>
      </div>
    ];
  }
}
