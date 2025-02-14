import streamlit as st
import pandas as pd
import numpy as np
import plotly.graph_objects as go
from datetime import datetime

"""def calculate_target(df):
    /
    Calcula el Target como: (Open(T+2) - Open(T+1)) * 100 / Open(T+1)
    /
    df['Next_Open'] = df['Open'].shift(-1)
    df['Next_Next_Open'] = df['Open'].shift(-2)
    df['Target'] = (df['Next_Next_Open'] - df['Next_Open']) * 100 / df['Next_Open']
    df = df.drop(['Next_Open', 'Next_Next_Open'], axis=1)
    return df.dropna()"""

"""def plot_price_evolution(df, date_column, train_start, train_end, test_start, test_end, forward_start, forward_end):
    /
    Crea un gráfico de la evolución del precio con diferentes colores por período
    /
    # Convertir todas las fechas a datetime64[ns]
    train_start = pd.to_datetime(train_start)
    train_end = pd.to_datetime(train_end)
    test_start = pd.to_datetime(test_start)
    test_end = pd.to_datetime(test_end)
    forward_start = pd.to_datetime(forward_start)
    forward_end = pd.to_datetime(forward_end)
    
    # Asegurar que la columna de fecha está en el formato correcto
    df[date_column] = pd.to_datetime(df[date_column])

    fig = go.Figure()

    # Test period
    mask_test = (df[date_column] >= test_start) & (df[date_column] <= test_end)
    df_test = df[mask_test]
    fig.add_trace(go.Scatter(
        x=df_test[date_column],
        y=df_test['Open'],
        name='Test',
        line=dict(color='#FFD700')  # amarillo
    ))

    # Train period
    mask_train = (df[date_column] >= train_start) & (df[date_column] <= train_end)
    df_train = df[mask_train]
    fig.add_trace(go.Scatter(
        x=df_train[date_column],
        y=df_train['Open'],
        name='Train',
        line=dict(color='#0000FF')  # azul
    ))

    # Forward period
    mask_forward = (df[date_column] >= forward_start) & (df[date_column] <= forward_end)
    df_forward = df[mask_forward]
    fig.add_trace(go.Scatter(
        x=df_forward[date_column],
        y=df_forward['Open'],
        name='Forward',
        line=dict(color='#FF0000')  # rojo
    ))

    fig.update_layout(
        title='Evolución del precio por período',
        xaxis_title='Fecha',
        yaxis_title='Precio de apertura',
        hovermode='x unified'
    )

    return fig"""

def create_returns_stats_table(train_returns, test_returns, forward_returns):
    """
    Crea una tabla con las estadísticas de los retornos
    """
    def calculate_stats(returns):
        if returns.empty:
            return {
                'mean': 0,
                'std': 0,
                'min': 0,
                'max': 0,
                'pos_pct': 0,
                'count': 0
            }
        
        stats = {
            'mean': returns.mean(),
            'std': returns.std(),
            'min': returns.min(),
            'max': returns.max(),
            'pos_pct': (returns > 0).mean() * 100,
            'count': len(returns)
        }
        return stats

    train_stats = calculate_stats(train_returns)
    test_stats = calculate_stats(test_returns)
    forward_stats = calculate_stats(forward_returns)
    
    stats_df = pd.DataFrame({
        'Estadística': [
            'Retorno Promedio (%)',
            'Desviación Estándar (%)',
            'Retorno Mínimo (%)',
            'Retorno Máximo (%)',
            '% Retornos Positivos',
            'Número de Observaciones'
        ],
        'Test': [
            f"{test_stats['mean']:.3f}",
            f"{test_stats['std']:.3f}",
            f"{test_stats['min']:.3f}",
            f"{test_stats['max']:.3f}",
            f"{test_stats['pos_pct']:.1f}",
            f"{test_stats['count']}"
        ],
        'Train': [
            f"{train_stats['mean']:.3f}",
            f"{train_stats['std']:.3f}",
            f"{train_stats['min']:.3f}",
            f"{train_stats['max']:.3f}",
            f"{train_stats['pos_pct']:.1f}",
            f"{train_stats['count']}"
        ],
        'Forward': [
            f"{forward_stats['mean']:.3f}",
            f"{forward_stats['std']:.3f}",
            f"{forward_stats['min']:.3f}",
            f"{forward_stats['max']:.3f}",
            f"{forward_stats['pos_pct']:.1f}",
            f"{forward_stats['count']}"
        ]
    }).set_index('Estadística')
    
    return stats_df

def select_uncorrelated_features(df, features, threshold=0.95):
    """
    Selecciona features con baja correlación de manera iterativa.
    Maneja casos de valores constantes y NaN.
    """
    if len(features) == 0:
        return []
    
    def safe_correlation(series1, series2):
        """
        Calcula correlación de manera segura, devolviendo 0 si hay error
        """
        try:
            # Eliminar filas donde cualquiera de las series tenga NaN
            mask = ~(series1.isna() | series2.isna())
            s1, s2 = series1[mask], series2[mask]
            
            # Verificar que hay suficiente varianza
            if s1.std() == 0 or s2.std() == 0:
                return 0
                
            return abs(s1.corr(s2))
        except:
            return 0
    
    selected_features = []
    
    # Verificar que el primer feature es válido
    first_feature = features[0]
    if df[first_feature].std() > 0 and df[first_feature].notna().any():
        selected_features = [first_feature]
        last_selected = first_feature
        
        # Iterar sobre el resto de features
        for feature in features[1:]:
            try:
                correlation = safe_correlation(df[feature], df[last_selected])
                if correlation < threshold:
                    selected_features.append(feature)
                    last_selected = feature
            except:
                continue
    
    return selected_features

def calculate_random_profits(returns, side='long', n_simulations=1000):
    """
    Calcula los profits aleatorios para el Monkey Test usando len(train)/3
    """
    side_multiplier = 1 if side == 'long' else -1
    random_metrics = []
    sample_size = int(len(returns)/3)
    
    for _ in range(n_simulations):
        sample = returns.sample(n=sample_size, replace=True)
        adjusted_sample = sample * side_multiplier
        mean = adjusted_sample.mean()
        std = adjusted_sample.std()
        metric = mean/std if std != 0 else 0
        random_metrics.append(metric)
    
    return np.array(random_metrics)

def analyze_feature(df, feature, target, side):
    """
    Analiza una feature dividiendo en terciles y retorna la métrica del mejor tercil
    junto con su regla correspondiente
    """
    side_multiplier = 1 if side == 'long' else -1
    adjusted_target = df[target] * side_multiplier
    
    try:
        tercile_edges = pd.qcut(df[feature], q=3, retbins=True)[1]
        terciles = pd.qcut(df[feature], q=3, labels=[0, 1, 2], duplicates='drop')
        
        metrics = []
        for i in range(3):
            tercil_returns = adjusted_target[terciles == i]
            if len(tercil_returns) > 0:
                mean = tercil_returns.mean()
                std = tercil_returns.std()
                metric = mean/std if std != 0 else 0
                metrics.append(metric)
            else:
                metrics.append(0)
        
        best_tercil = np.argmax(metrics)
        best_metric = metrics[best_tercil]
        
        if best_tercil == 0:
            rule = f"`{feature}` < {tercile_edges[1]:.3f}"
        elif best_tercil == 1:
            rule = f"`{feature}` >= {tercile_edges[1]:.3f} and `{feature}` < {tercile_edges[2]:.3f}"
        else:
            rule = f"`{feature}` >= {tercile_edges[2]:.3f}"
        
        return best_metric, rule
    except:
        return 0, ""

def analyze_all_features(df, target_column, date_column, side, correlation_threshold=0.7):
    """
    Analiza todas las features, primero reduciendo la multicolinealidad
    """
    exclude_columns = [date_column, 'Open', 'Close', target_column, 'Target']
    initial_features = [col for col in df.columns if col not in exclude_columns]
    
    selected_features = select_uncorrelated_features(df, initial_features, correlation_threshold)
    
    returns = df[target_column]
    random_profits = calculate_random_profits(returns, side)
    
    results = []
    rules_dict = {}
    
    for feature in selected_features:
        try:
            feature_metric, feature_rule = analyze_feature(df, feature, target_column, side)
            score = (feature_metric > random_profits).mean() * 100
            
            if score > 0:
                results.append({
                    'feature': feature,
                    'score': score
                })
                rules_dict[feature] = {
                    'rule': feature_rule,
                    'score': score
                }
        except:
            continue
    
    if results:
        results_df = pd.DataFrame(results)
        results_df = results_df.sort_values('score', ascending=False).reset_index(drop=True)
        results_df['score'] = results_df['score'].round(2)
        return results_df, rules_dict
    else:
        return pd.DataFrame(columns=['feature', 'score']), {}

def calculate_random_metrics_compound(returns, side='long', n_simulations=1000):
    """
    Calcula los monos para reglas compuestas usando len(train)/9
    """
    side_multiplier = 1 if side == 'long' else -1
    random_metrics = []
    sample_size = int(len(returns)/9)
    
    for _ in range(n_simulations):
        sample = returns.sample(n=sample_size, replace=True)
        adjusted_sample = sample * side_multiplier
        mean = adjusted_sample.mean()
        std = adjusted_sample.std()
        metric = mean/std if std != 0 else 0
        random_metrics.append(metric)
    
    return np.array(random_metrics)

def plot_monkey_distribution(random_metrics, threshold, period='train'):
    """
    Crea un histograma de los monos con línea vertical en el umbral
    
    Args:
        random_metrics: array con las métricas de los monos
        threshold: umbral para la línea vertical
        period: 'train' o 'test' para indicar el período
    """
    fig = go.Figure()
    
    fig.add_trace(go.Histogram(
        x=random_metrics,
        name='Random Metrics',
        nbinsx=50,
        histnorm='probability'
    ))

    fig.add_vline(
        x=threshold,
        line_dash="dash",
        line_color="red",
        annotation_text=f"Threshold (99%): {threshold:.3f}"
    )

    fig.update_layout(
        title=f'Monkey Test ({period})',
        xaxis_title='Métrica',
        yaxis_title='Frecuencia',
        showlegend=False
    )
    
    return fig

def find_second_rule(train_data, first_feature, first_rule, target_column, date_column, side, threshold, filtered_features):
    """
    Encuentra segundas reglas analizando solo las variables que pasaron el filtro inicial
    """
    submask = train_data.query(first_rule)
    
    if len(submask) < 30:
        return None
    
    features_to_analyze = [f for f in filtered_features if f != first_feature]
    compound_rules = []
    
    for feature in features_to_analyze:
        try:
            tercile_edges = pd.qcut(submask[feature], q=3, retbins=True, duplicates='drop')[1]
            
            for i in range(3):
                if i == 0:
                    second_rule = f"`{feature}` < {tercile_edges[1]:.3f}"
                elif i == 1:
                    second_rule = f"`{feature}` >= {tercile_edges[1]:.3f} and `{feature}` < {tercile_edges[2]:.3f}"
                else:
                    second_rule = f"`{feature}` >= {tercile_edges[2]:.3f}"
                
                compound_rule = f"({first_rule}) and ({second_rule})"
                rule_mask = train_data.eval(compound_rule)
                
                if len(train_data[rule_mask]) >= 30:
                    side_multiplier = 1 if side == 'long' else -1
                    returns = train_data[rule_mask][target_column] * side_multiplier
                    mean = returns.mean()
                    std = returns.std()
                    metric = mean/std if std != 0 else 0
                    
                    if metric > threshold:
                        compound_rules.append({
                            'feature': [first_feature, feature],
                            'rule': compound_rule,
                            'metric': metric,
                            'train_metric': metric
                        })
        except:
            continue
    
    if compound_rules:
        results_df = pd.DataFrame(compound_rules)
        return results_df.sort_values('train_metric', ascending=False).reset_index(drop=True)
    else:
        return None

def plot_rule_returns(df, rule, date_column, side, mask_train):
    """
    Grafica la evolución de los retornos acumulados para una regla en train y test
    """
    mask = df.eval(rule)
    df_filtered = df[mask].copy()
    
    if len(df_filtered) == 0:
        return None
        
    side_multiplier = 1 if side == 'long' else -1
    df_filtered['adjusted_returns'] = df_filtered['Target'] * side_multiplier
    
    # Asignar período (train/test) a cada fila
    df_filtered['period'] = 'test'
    df_filtered.loc[mask_train, 'period'] = 'train'
    
    # Calcular retornos acumulados por período
    df_filtered['cumulative_returns'] = df_filtered['adjusted_returns'].cumsum()
    
    fig = go.Figure()
    
    # Graficar train
    df_train = df_filtered[df_filtered['period'] == 'train']
    if not df_train.empty:
        fig.add_trace(go.Scatter(
            x=df_train[date_column],
            y=df_train['cumulative_returns'],
            mode='lines',
            name='Train',
            line=dict(color='blue')
        ))
    
    # Graficar test
    df_test = df_filtered[df_filtered['period'] == 'test']
    if not df_test.empty:
        fig.add_trace(go.Scatter(
            x=df_test[date_column],
            y=df_test['cumulative_returns'],
            mode='lines',
            name='Test',
            line=dict(color='gold')
        ))
    
    fig.update_layout(
        title='Evolución del Retorno Acumulado',
        xaxis_title='Fecha',
        yaxis_title='Retorno Acumulado (%)',
        showlegend=True
    )
    
    return fig

def calculate_rule_metric(df, rule, target_column, side):
    """
    Calcula la métrica (mean/std) para una regla específica
    """
    try:
        rule_mask = df.eval(rule)
        if len(df[rule_mask]) < 30:
            return 0
            
        side_multiplier = 1 if side == 'long' else -1
        returns = df[rule_mask][target_column] * side_multiplier
        mean = returns.mean()
        std = returns.std()
        return mean/std if std != 0 else 0
    except:
        return 0

def validate_rules(df_test, rules_df, target_column, side, random_metrics):
    """
    Valida las reglas contra la distribución de monos en test
    """
    validation_results = []
    
    for _, row in rules_df.iterrows():
        rule = row['rule']
        train_metric = row['metric'] if 'metric' in row else 0
        
        # Calcular métrica en test
        test_metric = calculate_rule_metric(df_test, rule, target_column, side)
        # Calcular porcentaje de monos superados
        monkey_percentile = (test_metric > random_metrics).mean() * 100
        
        validation_results.append({
            'rule': rule,
            'train_metric': train_metric,
            'test_metric': test_metric,
            'validation': monkey_percentile
        })
    
    return pd.DataFrame(validation_results)

def plot_metric_comparison(df):
    """
    Crea un gráfico de dispersión comparando métricas de train vs test
    """
    if 'train_metric' not in df.columns or 'test_metric' not in df.columns:
        return None
        
    fig = go.Figure()
    
    fig.add_trace(go.Scatter(
        x=df['train_metric'],
        y=df['test_metric'],
        mode='markers',
        marker=dict(
            size=10,
            color='blue',
            opacity=0.6
        ),
        name='Reglas'
    ))
    
    # Añadir línea diagonal de referencia
    max_value = max(max(df['train_metric']), max(df['test_metric']))
    min_value = min(min(df['train_metric']), min(df['test_metric']))
    fig.add_trace(go.Scatter(
        x=[min_value, max_value],
        y=[min_value, max_value],
        mode='lines',
        line=dict(dash='dash', color='red'),
        name='Línea de referencia'
    ))
    
    fig.update_layout(
        title='Comparación de Métricas Train vs Test',
        xaxis_title='Métrica en Train',
        yaxis_title='Métrica en Test',
        showlegend=True
    )
    
    return fig

def plot_ks_test(train_returns, test_returns):
    """
    Realiza el test KS y crea una visualización comparativa
    Returns:
        fig: figura de plotly
        ks_statistic: estadístico KS
        p_value: p-valor del test
    """
    from scipy import stats
    
    # Realizar test KS
    ks_statistic, p_value = stats.ks_2samp(train_returns, test_returns)
    
    # Crear CDFs empíricas
    def empirical_cdf(data):
        x = np.sort(data)
        y = np.arange(1, len(data) + 1) / len(data)
        return x, y
    
    x_train, y_train = empirical_cdf(train_returns)
    x_test, y_test = empirical_cdf(test_returns)
    
    # Encontrar el punto de máxima diferencia
    def find_max_diff_point(x1, y1, x2, y2):
        # Combinar todos los puntos x y ordenarlos
        all_x = np.sort(np.unique(np.concatenate([x1, x2])))
        
        # Interpolar los valores y para cada conjunto
        y1_interp = np.interp(all_x, x1, y1)
        y2_interp = np.interp(all_x, x2, y2)
        
        # Encontrar la diferencia máxima
        diff = np.abs(y1_interp - y2_interp)
        max_diff_idx = np.argmax(diff)
        
        return all_x[max_diff_idx], y1_interp[max_diff_idx], y2_interp[max_diff_idx]
    
    x_max, y1_max, y2_max = find_max_diff_point(x_train, y_train, x_test, y_test)
    
    # Crear gráfico
    fig = go.Figure()
    
    # Añadir CDFs
    fig.add_trace(go.Scatter(
        x=x_train, y=y_train,
        name='Train CDF',
        line=dict(color='blue')
    ))
    
    fig.add_trace(go.Scatter(
        x=x_test, y=y_test,
        name='Test CDF',
        line=dict(color='gold')
    ))
    
    # Añadir línea de máxima diferencia
    fig.add_trace(go.Scatter(
        x=[x_max, x_max],
        y=[y1_max, y2_max],
        mode='lines',
        name='KS Distance',
        line=dict(color='red', dash='dash')
    ))
    
    # Actualizar layout
    fig.update_layout(
        title=f'Test Kolmogorov-Smirnov<br>Estadístico: {ks_statistic:.3f}, p-valor: {p_value:.3e}',
        xaxis_title='Retornos',
        yaxis_title='Probabilidad Acumulada',
        showlegend=True
    )
    
    # Añadir anotación interpretativa
    interpretation = "Las distribuciones son significativamente diferentes" if p_value < 0.05 else "No hay evidencia de diferencias significativas"
    fig.add_annotation(
        text=interpretation,
        xref="paper", yref="paper",
        x=0.5, y=1.05,
        showarrow=False,
        font=dict(size=12)
    )
    
    return fig, ks_statistic, p_value

def calculate_backtest_metrics(df, rule, side='long'):
    """
    Calcula métricas de backtest para una regla
    """
    # Aplicar la regla
    mask = df.eval(rule)
    trades = df[mask].copy()
    
    if len(trades) == 0:
        return None
        
    # Ajustar por side
    side_multiplier = 1 if side == 'long' else -1
    trades['adjusted_returns'] = trades['Target'] * side_multiplier
    
    # Calcular métricas
    metrics = {
        'total_trades': len(trades),
        'total_return': trades['adjusted_returns'].sum(),
        'avg_return': trades['adjusted_returns'].mean(),
        'win_rate': (trades['adjusted_returns'] > 0).mean() * 100,
        'best_trade': trades['adjusted_returns'].max(),
        'worst_trade': trades['adjusted_returns'].min(),
        'std_returns': trades['adjusted_returns'].std(),
        'sharpe': trades['adjusted_returns'].mean() / trades['adjusted_returns'].std() if trades['adjusted_returns'].std() != 0 else 0
    }
    
    # Calcular drawdown
    trades['cumulative_returns'] = trades['adjusted_returns'].cumsum()
    trades['rolling_max'] = trades['cumulative_returns'].cummax()
    trades['drawdown'] = trades['rolling_max'] - trades['cumulative_returns']
    metrics['max_drawdown'] = trades['drawdown'].max()
    
    return metrics, trades

def plot_backtest_equity_curve(trades, date_column):
    """
    Crea un gráfico de la equity curve con drawdown
    """
    fig = go.Figure()
    
    # Equity curve
    fig.add_trace(go.Scatter(
        x=trades[date_column],
        y=trades['cumulative_returns'],
        name='Equity Curve',
        line=dict(color='blue')
    ))
    
    # Rolling maximum
    fig.add_trace(go.Scatter(
        x=trades[date_column],
        y=trades['rolling_max'],
        name='High Water Mark',
        line=dict(color='green', dash='dot')
    ))
    
    fig.update_layout(
        title='Equity Curve y High Water Mark',
        xaxis_title='Fecha',
        yaxis_title='Retorno Acumulado (%)',
        showlegend=True
    )
    
    return fig

def plot_trades_distribution(trades):
    """
    Crea un histograma de los retornos de los trades
    """
    fig = go.Figure()
    
    fig.add_trace(go.Histogram(
        x=trades['adjusted_returns'],
        nbinsx=50,
        name='Distribución de Retornos'
    ))
    
    fig.update_layout(
        title='Distribución de Retornos por Trade',
        xaxis_title='Retorno (%)',
        yaxis_title='Frecuencia',
        showlegend=False
    )
    
    return fig

def plot_returns_waterfall(returns):
    """
    Crea un gráfico de barras que muestra la suma de retornos positivos y negativos
    """
    # Calcular sumas
    positive_returns = returns[returns > 0].sum()
    negative_returns = returns[returns <= 0].sum()
    net_return = positive_returns + negative_returns
    
    # Crear gráfico
    fig = go.Figure()
    
    # Barra de retornos positivos
    fig.add_trace(go.Bar(
        name='Retornos Positivos',
        x=['Retornos Positivos'],
        y=[positive_returns],
        marker_color='#2ecc71',
        text=f'+{positive_returns:.2f}%',
        textposition='outside'
    ))
    
    # Barra de retornos negativos
    fig.add_trace(go.Bar(
        name='Retornos Negativos',
        x=['Retornos Negativos'],
        y=[negative_returns],
        marker_color='#e74c3c',
        text=f'{negative_returns:.2f}%',
        textposition='outside'
    ))
    
    # Barra de retorno neto
    fig.add_trace(go.Bar(
        name='Retorno Neto',
        x=['Retorno Neto'],
        y=[net_return],
        marker_color='#3498db',
        text=f'{net_return:.2f}%',
        textposition='outside'
    ))
    
    # Actualizar layout
    fig.update_layout(
        title='Suma de Retornos (Train + Test)',
        yaxis_title='Retorno (%)',
        showlegend=True,
        barmode='group'
    )
    
    # Añadir anotaciones con porcentajes sobre total
    total_abs = abs(positive_returns) + abs(negative_returns)
    pos_pct = (abs(positive_returns) / total_abs * 100)
    neg_pct = (abs(negative_returns) / total_abs * 100)
    
    fig.add_annotation(
        x='Retornos Positivos',
        y=positive_returns,
        text=f'({pos_pct:.1f}% del volumen)',
        showarrow=False,
        yshift=30
    )
    
    fig.add_annotation(
        x='Retornos Negativos',
        y=negative_returns,
        text=f'({neg_pct:.1f}% del volumen)',
        showarrow=False,
        yshift=-30
    )
    
    return fig

def main():
    # Título principal con emoji
    st.markdown("# 🤖 RULE EXTRACTOR")
    st.markdown("---")

    # Crear tabs
    tab_data, tab_features, tab_rules, tab_validation, tab_forward, tab_backtest = st.tabs([
    "Data", "Feature Selection", "Rule Extraction", "Validation", "Forward", "Backtest"
    ])

# TAB DATA
    with tab_data:
        uploaded_file = st.file_uploader(
            "Arrastra y suelta tu archivo CSV aquí",
            type=["csv"]
        )
        
        if uploaded_file is not None:
            try:
                # Leer el archivo y procesar datos
                df = pd.read_csv(uploaded_file)
                df = calculate_target(df)
                
                # Mostrar el DataFrame
                st.write("### Vista previa de los datos")
                st.dataframe(df)

                # Detectar columna de fecha
                date_columns = df.select_dtypes(include=['datetime64']).columns
                if len(date_columns) == 0:
                    potential_date_cols = [col for col in df.columns if 'date' in col.lower() or 'fecha' in col.lower()]
                    if potential_date_cols:
                        df[potential_date_cols[0]] = pd.to_datetime(df[potential_date_cols[0]])
                        date_column = potential_date_cols[0]
                    else:
                        st.error("No se encontró una columna de fecha. Por favor, asegúrate de que tu CSV tiene una columna de fecha.")
                        return
                else:
                    date_column = date_columns[0]

                # Obtener el rango de fechas
                min_date = pd.to_datetime(df[date_column].min())
                max_date = pd.to_datetime(df[date_column].max())
                
                st.write("### Selección de períodos")
                st.write(f"Rango de fechas disponible: {min_date.strftime('%Y-%m-%d')} a {max_date.strftime('%Y-%m-%d')}")

                # Fechas por defecto
                default_dates = {
                    'test_start': min_date,
                    'test_end': pd.to_datetime('2020-01-01'),
                    'train_start': pd.to_datetime('2020-01-01'),
                    'train_end': pd.to_datetime('2023-01-01'),
                    'forward_start': pd.to_datetime('2023-01-01'),
                    'forward_end': max_date
                }

                # Contenedor para los selectores de fecha
                with st.container():
                    col1, col2, col3 = st.columns(3)

                    with col1:
                        st.markdown("### Test")
                        test_start = st.date_input(
                            "Inicio Test",
                            default_dates['test_start'].date(), 
                            min_value=min_date.date(), 
                            max_value=max_date.date(), 
                            key="test_start"
                        )
                        test_end = st.date_input(
                            "Fin Test",
                            default_dates['test_end'].date(), 
                            min_value=min_date.date(), 
                            max_value=max_date.date(), 
                            key="test_end"
                        )

                    with col2:
                        st.markdown("### Train")
                        train_start = st.date_input(
                            "Inicio Train",
                            default_dates['train_start'].date(), 
                            min_value=min_date.date(), 
                            max_value=max_date.date(), 
                            key="train_start"
                        )
                        train_end = st.date_input(
                            "Fin Train",
                            default_dates['train_end'].date(), 
                            min_value=min_date.date(), 
                            max_value=max_date.date(), 
                            key="train_end"
                        )

                    with col3:
                        st.markdown("### Forward")
                        forward_start = st.date_input(
                            "Inicio Forward",
                            default_dates['forward_start'].date(), 
                            min_value=min_date.date(), 
                            max_value=max_date.date(), 
                            key="forward_start"
                        )
                        forward_end = st.date_input(
                            "Fin Forward",
                            default_dates['forward_end'].date(), 
                            min_value=min_date.date(), 
                            max_value=max_date.date(), 
                            key="forward_end"
                        )

                # Convertir todas las fechas a datetime para las máscaras
                test_start_dt = pd.to_datetime(test_start)
                test_end_dt = pd.to_datetime(test_end)
                train_start_dt = pd.to_datetime(train_start)
                train_end_dt = pd.to_datetime(train_end)
                forward_start_dt = pd.to_datetime(forward_start)
                forward_end_dt = pd.to_datetime(forward_end)

                # Crear y guardar todas las máscaras en session_state
                st.session_state['mask_train'] = (df[date_column] >= train_start_dt) & (df[date_column] <= train_end_dt)
                st.session_state['mask_test'] = (df[date_column] >= test_start_dt) & (df[date_column] <= test_end_dt)
                st.session_state['mask_forward'] = (df[date_column] >= forward_start_dt) & (df[date_column] <= forward_end_dt)

                train_returns = df[st.session_state['mask_train']]['Target']
                test_returns = df[st.session_state['mask_test']]['Target']
                forward_returns = df[st.session_state['mask_forward']]['Target']

                # Guardar los returns en session_state
                st.session_state['train_returns'] = train_returns
                st.session_state['test_returns'] = test_returns
                st.session_state['forward_returns'] = forward_returns

                # Mostrar gráfico de evolución de precios
                st.write("### Evolución del precio")
                price_fig = plot_price_evolution(
                    df, 
                    date_column,
                    train_start,
                    train_end,
                    test_start,
                    test_end,
                    forward_start,
                    forward_end
                )
                st.plotly_chart(price_fig)

                # Estadísticas descriptivas
                st.write("### Estadísticas de los retornos")
                stats_df = create_returns_stats_table(train_returns, test_returns, forward_returns)
                st.dataframe(stats_df, use_container_width=True)

# Test KS y su visualización
                st.write("### Comparación de Distribuciones Train-Test")
                ks_fig, ks_stat, p_value = plot_ks_test(train_returns, test_returns)
                st.plotly_chart(ks_fig)

                if p_value < 0.05:
                    st.warning("""
                        ⚠️ Las distribuciones de train y test son significativamente diferentes.
                        Los resultados de la validación podrían no ser representativos.
                    """)

                # Añadir waterfall plot para train+test combinados
                st.write("### Análisis de Magnitud de Retornos (Train + Test)")
                combined_returns = pd.concat([train_returns, test_returns])
                waterfall_fig = plot_returns_waterfall(combined_returns)
                st.plotly_chart(waterfall_fig)

                # Calcular el retorno neto para las conclusiones
                net_return = combined_returns.sum()

                # Añadir conclusiones
                st.write("### Conclusiones del Análisis")
                
                st.write("📊 **Estabilidad del Activo**")
                if p_value > 0.60:
                    st.success("🌟 **Activo Muy Adecuado para Minería de Reglas**\n\n"
                             "Las distribuciones de train y test son altamente similares, "
                             "lo que sugiere una gran estabilidad en el comportamiento del activo.")
                elif p_value > 0.30:
                    st.info("✅ **Activo Adecuado para Minería de Reglas**\n\n"
                          "Existe una similitud razonable entre las distribuciones de train y test, "
                          "permitiendo la búsqueda de reglas con cierta confianza.")
                else:
                    st.warning("⚠️ **Activo Poco Adecuado para Minería de Reglas**\n\n"
                             "Las diferencias significativas entre train y test sugieren "
                             "inestabilidad en el comportamiento del activo.")

                st.write("📈 **Comportamiento Tendencial**")
                if net_return > 15:
                    st.info("📈 **Sesgo Alcista Significativo**\n\n"
                          "El activo muestra una fuerte tendencia alcista en el período analizado, "
                          "con un retorno neto superior al 15%.")
                elif net_return < -15:
                    st.info("📉 **Sesgo Bajista Significativo**\n\n"
                          "El activo muestra una fuerte tendencia bajista en el período analizado, "
                          "con un retorno neto inferior al -15%.")
                else:
                    st.info("↔️ **Sin Sesgo Tendencial Claro**\n\n"
                          "El activo no muestra un sesgo tendencial significativo, "
                          "con un retorno neto entre -15% y 15%.")

                # Guardar variables en session_state para usar en otras tabs
                st.session_state['df'] = df
                st.session_state['date_column'] = date_column

            except Exception as e:
                st.error(f"Error al procesar el archivo: {str(e)}")

# TAB FEATURE SELECTION
    with tab_features:
        if 'df' in st.session_state:
            st.subheader("Selección de Features")
            
            col1, col2 = st.columns(2)
            
            with col1:
                # Selector para side
                side = st.selectbox(
                    'Selecciona Side',
                    options=['long', 'short'],
                    help='Dirección de la estrategia'
                )
            
            with col2:
                # Selector para umbral de correlación
                correlation_threshold = st.slider(
                    'Umbral de Correlación',
                    min_value=0.75,
                    max_value=1.0,
                    value=0.95,
                    step=0.01,
                    help='Umbral máximo de correlación permitido entre features. Un valor más bajo resulta en menos features pero más independientes entre sí.'
                )
            
            # Calcular todas las features solo para el período de train
            if st.button('Analizar Features'):
                with st.spinner('Analizando features...'):
                    # Obtener features y sus scores
                    features_df, rules_dict = analyze_all_features(
                        st.session_state['df'][st.session_state['mask_train']], 
                        'Target',
                        st.session_state['date_column'],
                        side,
                        correlation_threshold
                    )
                    
                    # Guardar reglas en session state para Rule Extraction
                    st.session_state['primary_rules'] = rules_dict
                    st.session_state['side'] = side
                    
                    if not features_df.empty:
                        st.write(f"Features seleccionadas después de aplicar el filtro de correlación (umbral: {correlation_threshold}):")
                        st.dataframe(features_df, use_container_width=True)
                    else:
                        st.warning("No se encontraron features significativas")
        else:
            st.info("Por favor, carga un archivo CSV en la pestaña Data primero.")

    # TAB RULE EXTRACTION
    with tab_rules:
        if 'df' in st.session_state and 'primary_rules' in st.session_state:
            st.subheader("Extracción de Reglas Compuestas")
            
            # Calcular monkey test solo la primera vez
            if 'compound_threshold' not in st.session_state:
                with st.spinner('Calculando distribución de monos...'):
                    random_metrics = calculate_random_metrics_compound(
                        st.session_state['train_returns'],
                        st.session_state['side']
                    )
                    threshold = np.quantile(random_metrics, 0.99)
                    st.session_state['compound_threshold'] = threshold
                    st.session_state['random_metrics'] = random_metrics
            
            # Mostrar distribución de monos
            st.plotly_chart(plot_monkey_distribution(
                st.session_state['random_metrics'],
                st.session_state['compound_threshold']
            ))
            
            # Obtener features que pasaron el filtro inicial
            features_df = pd.DataFrame([
                {'feature': k, 'score': v['score']} 
                for k, v in st.session_state['primary_rules'].items()
            ]).sort_values('score', ascending=False)
            
            # Lista de todas las features filtradas
            filtered_features = features_df['feature'].tolist()
            
            selected_feature = st.selectbox(
                'Selecciona una feature base:',
                options=filtered_features,
                format_func=lambda x: f"{x} (Score: {features_df[features_df['feature'] == x]['score'].iloc[0]:.2f})"
            )
            
            # Mostrar cuántas features hay disponibles para la segunda regla
            st.info(f"Se buscarán reglas compuestas usando las {len(filtered_features)-1} features restantes que pasaron el filtro inicial.")
            
            # Botón para buscar reglas
            if st.button('Buscar Reglas'):
                with st.spinner('Buscando reglas compuestas...'):
                    # Obtener regla base
                    base_rule = st.session_state['primary_rules'][selected_feature]['rule']
                    
                    # Encontrar reglas compuestas
                    compound_rules_df = find_second_rule(
                        st.session_state['df'][st.session_state['mask_train']],
                        selected_feature,
                        base_rule,
                        'Target',
                        st.session_state['date_column'],
                        st.session_state['side'],
                        st.session_state['compound_threshold'],
                        filtered_features  # Pasamos la lista de features filtradas
                    )
                    
                    # Guardar reglas en session_state
                    st.session_state['compound_rules_df'] = compound_rules_df
            
            # Mostrar reglas y evolución si existen
            if 'compound_rules_df' in st.session_state:
                compound_rules_df = st.session_state['compound_rules_df']
                if compound_rules_df is not None and not compound_rules_df.empty:
                    st.dataframe(compound_rules_df, use_container_width=True)
                    
                    # Selector de regla para visualización
                    selected_rule_index = st.selectbox(
                        'Selecciona una regla para ver su evolución:',
                        options=range(len(compound_rules_df)),
                        format_func=lambda x: f"Métrica: {compound_rules_df.iloc[x]['metric']:.3f} | {compound_rules_df.iloc[x]['rule']}"
                    )
                    
                    # En Rule Extraction, mostrar evolución de la regla seleccionada
                    rule = compound_rules_df.iloc[selected_rule_index]['rule']
                    fig = plot_rule_returns(
                        st.session_state['df'][st.session_state['mask_train']],
                        rule,
                        st.session_state['date_column'],
                        st.session_state['side'],
                        st.session_state['mask_train']  # Añadido el nuevo parámetro
                    )
                    
                    if fig is not None:
                        st.plotly_chart(fig)
                    else:
                        st.warning("No hay suficientes datos para mostrar la evolución")
                else:
                    st.warning("No se encontraron reglas compuestas que superen el umbral")
        else:
            st.info("Por favor, completa el análisis de features primero")

# TAB VALIDATION
    with tab_validation:
        # Verificar que tenemos todos los datos necesarios
        required_keys = ['df', 'compound_rules_df', 'mask_test', 'test_returns', 'side']
        missing_keys = [key for key in required_keys if key not in st.session_state]
        
        if missing_keys:
            st.info(f"Por favor, completa los pasos anteriores primero. Faltan los siguientes datos: {', '.join(missing_keys)}")
        else:
            # Calcular monkey test para test solo la primera vez
            if 'test_random_metrics' not in st.session_state:
                with st.spinner('Calculando distribución de monos para test...'):
                    df_test = st.session_state['df'][st.session_state['mask_test']]
                    test_returns = df_test['Target']
                    
                    random_metrics = calculate_random_metrics_compound(
                        test_returns,
                        st.session_state['side']
                    )
                    st.session_state['test_random_metrics'] = random_metrics
            
            # Validar reglas si existen
            compound_rules_df = st.session_state['compound_rules_df']
            if compound_rules_df is not None and not compound_rules_df.empty:
                with st.spinner('Validando reglas...'):
                    df_test = st.session_state['df'][st.session_state['mask_test']]
                    
                    # Validar las reglas
                    validation_df = validate_rules(
                        df_test,
                        compound_rules_df,
                        'Target',
                        st.session_state['side'],
                        st.session_state['test_random_metrics']
                    )
                    
                    # Preparar DataFrame para mostrar
                    display_df = validation_df[['rule', 'validation']].copy()
                    display_df['validation'] = display_df['validation'].round(2)
                    display_df = display_df.sort_values('validation', ascending=False)

                    # Guardar en session_state para usar en Forward
                    st.session_state['validation_df'] = display_df
                    
                    # Mostrar tabla
                    st.dataframe(display_df, use_container_width=True)
                    
                    # Permitir seleccionar una regla para ver su evolución en test
                    if len(display_df) > 0:
                        selected_rule = st.selectbox(
                            'Selecciona una regla para ver su evolución en test:',
                            options=display_df['rule'].tolist()
                        )
                        
                        # Obtener datos de train y test
                        mask_combined = st.session_state['mask_train'] | st.session_state['mask_test']
                        df_combined = st.session_state['df'][mask_combined]
                        
                        # Mostrar evolución de la regla seleccionada
                        fig = plot_rule_returns(
                            df_combined,
                            selected_rule,
                            st.session_state['date_column'],
                            st.session_state['side'],
                            st.session_state['mask_train']
                        )
                        
                        if fig is not None:
                            st.plotly_chart(fig)
                        else:
                            st.warning("No hay suficientes datos para mostrar la evolución")
            else:
                st.warning("No hay reglas para validar. Por favor, genera reglas en la pestaña Rule Extraction primero.")

# TAB FORWARD
    with tab_forward:
        # Verificar datos necesarios
        required_keys = ['df', 'validation_df', 'mask_forward']
        missing_keys = [key for key in required_keys if key not in st.session_state]
        
        if missing_keys:
            st.info(f"Por favor, completa los pasos anteriores primero. Faltan los siguientes datos: {', '.join(missing_keys)}")
        else:
            # Selector para el umbral de validación
            validation_threshold = st.slider(
                'Umbral de Validación',
                min_value=0.0,
                max_value=100.0,
                value=90.0,
                step=5.0,
                help='Selecciona el umbral mínimo de validación para filtrar las reglas'
            )
            
            # Guardar el umbral en session_state
            st.session_state['validation_threshold'] = validation_threshold
            
            # Filtrar reglas que superan el umbral
            filtered_rules = st.session_state['validation_df'][
                st.session_state['validation_df']['validation'] >= validation_threshold
            ].copy()
            
            if not filtered_rules.empty:
                st.write(f"Reglas que superan el umbral de validación ({validation_threshold}%):")
                st.dataframe(filtered_rules[['rule', 'validation']].sort_values('validation', ascending=False), use_container_width=True)
                
                # Selector de regla para visualización
                selected_rule = st.selectbox(
                    'Selecciona una regla para ver su evolución en forward:',
                    options=filtered_rules['rule'].tolist()
                )
                
                # Mostrar evolución de la regla seleccionada en forward
                fig = plot_rule_returns(
                    st.session_state['df'][st.session_state['mask_forward']],
                    selected_rule,
                    st.session_state['date_column'],
                    st.session_state['side'],
                    pd.Series(False, index=st.session_state['df'][st.session_state['mask_forward']].index)
                )
                
                if fig is not None:
                    st.plotly_chart(fig)
                else:
                    st.warning("No hay suficientes datos para mostrar la evolución")
            else:
                st.warning(f"No hay reglas que superen el umbral de validación de {validation_threshold}%")

# TAB BACKTEST
    with tab_backtest:
        # Verificar datos necesarios
        required_keys = ['df', 'validation_df', 'date_column', 'side', 'validation_threshold']
        missing_keys = [key for key in required_keys if key not in st.session_state]
        
        if missing_keys:
            st.info("Por favor, selecciona primero un umbral de validación en la pestaña Forward")
        else:
            # Usar el umbral definido en Forward
            validation_threshold = st.session_state['validation_threshold']
            filtered_rules = st.session_state['validation_df'][
                st.session_state['validation_df']['validation'] >= validation_threshold
            ]
            
            if not filtered_rules.empty:
                st.write(f"### Reglas que superan el umbral de validación ({validation_threshold}%)")
                
                # Seleccionar regla
                selected_rule = st.selectbox(
                    'Selecciona una regla para el backtest:',
                    options=filtered_rules['rule'].tolist()
                )
                
                # Obtener datos completos
                mask_all = st.session_state['mask_train'] | st.session_state['mask_test'] | st.session_state['mask_forward']
                df_all = st.session_state['df'][mask_all].copy()
                
                # Calcular métricas
                metrics, trades = calculate_backtest_metrics(
                    df_all, 
                    selected_rule, 
                    st.session_state['side']
                )
                
                if metrics:
                    # Mostrar métricas en dos columnas
                    col1, col2 = st.columns(2)
                    
                    with col1:
                        st.write("### Métricas de Trading")
                        st.write(f"Número de trades: {metrics['total_trades']}")
                        st.write(f"Retorno total: {metrics['total_return']:.2f}%")
                        st.write(f"Retorno medio: {metrics['avg_return']:.2f}%")
                        st.write(f"Ratio de aciertos: {metrics['win_rate']:.1f}%")
                    
                    with col2:
                        st.write("### Métricas de Riesgo")
                        st.write(f"Sharpe ratio: {metrics['sharpe']:.2f}")
                        st.write(f"Máximo drawdown: {metrics['max_drawdown']:.2f}%")
                        st.write(f"Mejor trade: {metrics['best_trade']:.2f}%")
                        st.write(f"Peor trade: {metrics['worst_trade']:.2f}%")
                    
                    # Mostrar gráficos
                    st.write("### Equity Curve")
                    equity_fig = plot_backtest_equity_curve(trades, st.session_state['date_column'])
                    st.plotly_chart(equity_fig)
                    
                    st.write("### Distribución de Retornos")
                    dist_fig = plot_trades_distribution(trades)
                    st.plotly_chart(dist_fig)
                    
                else:
                    st.warning("No hay suficientes trades para calcular las métricas")
            else:
                st.warning(f"No hay reglas que superen el umbral de validación de {validation_threshold}%")

if __name__ == "__main__":
    main()