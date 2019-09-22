import React, { Component } from 'react';
import { render } from 'react-dom';
import HighchartsReact from 'highcharts-react-official';
import Highcharts from 'highcharts';

class LineChart extends Component {
    constructor(props) {
        super(props);

        this.state = {
            chartOptions: null,
            GraphKeyword: this.props.GraphKeyword,
            isFetching: true
        };
    }

    async componentDidMount() {
        await fetch('https://localhost:5001/api/RecurringKeywordPosition/' + this.state.GraphKeyword.RecurringKeywordId)
            .then(response => response.json())
            .then(dataFetched => {
                this.setState({
                    chartOptions: {
                        title: {
                            text: 'Evolution for the keyword ' + this.state.GraphKeyword.Query + ' with Url ' + this.state.GraphKeyword.Url + ' at google.' + this.state.GraphKeyword.CountryDomain
                        },
                        xAxis: {
                            categories: dataFetched.x,
                        },
                        series: [
                            {
                                name: 'Keyword evolution',
                                data: dataFetched.y
                            }
                        ]
                    },
                    isFetching: false
                });
            });

    }

    render() {
        if (this.state.isFetching) return null;
        const { chartOptions } = this.state;

        return (
            <div className="card">
                <HighchartsReact
                    highcharts={Highcharts}
                    options={chartOptions}
                />
            </div>
        )
    }
}

export default LineChart;