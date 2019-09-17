import React, { Component } from 'react';

export class Home extends Component {
  static displayName = Home.name;

  render () {
    return (
      <div>
            <div class="form-row">
                <div class="form-group col-md-3">
                    <label for="inputQuery">Query</label>
                    <input type="text" class="form-control" id="inputQuery" />
                </div>
                <div class="form-group col-md-5">
                    <label for="inputUrl">Url</label>
                    <input type="text" class="form-control" id="inputUrl" />
                </div>
                <div class="form-group col-md-4">
                    <label for="inputCountryDomain">Country Domain</label>
                    <select id="inputCountryDomain" class="form-control" required>
                        <option value="co.uk">co.uk</option>
                        <option value="co.uk">com</option>
                        <option value="co.au">co.au</option>
                    </select>
                </div>
            </div>
            <div class="form-group row">
                <div class="col-sm-10">
                    <button type="submit" class="btn btn-primary">Get Positions</button>
                </div>
            </div>
      </div>
    );
  }
}
